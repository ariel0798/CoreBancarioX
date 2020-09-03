using AutoMapper;
using CB.AplicationCore.Constants;
using CB.AplicationCore.Interfaces;
using CB.AplicationCore.Interfaces.Validations;
using CB.Common.DTOs.DtoIn;
using CB.Common.DTOs.DtoOut;
using CB.Common.Enums;
using CB.Common.Models;
using CB.Domain.Interfaces;
using CB.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CB.AplicationCore.Services
{
    public class TransaccionService: ITransaccionService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly ITransaccionValidationService transaccionValidationService;
        readonly IProductoValidationService productoValidationService;

        public TransaccionService(IMasterRepository masterRepository, IMapper mapper, 
            ITransaccionValidationService transaccionValidationService,
            IProductoValidationService productoValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.transaccionValidationService = transaccionValidationService;
            this.productoValidationService = productoValidationService;
        }


        public ServiceResult<TransaccionResponseDtoOut> CreateTransaction(TransaccionDtoIn transaccionDto) 
        {
            try
            {
                if (!productoValidationService.IsExistingProductoId(transaccionDto.ProductoOrigenId))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingProductoOrigenId);

                if (!productoValidationService.IsExistingProductoId(transaccionDto.ProductoDestinoId))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingProductoDestinoId);

                if (transaccionDto.Monto <= 0)
                    throw new ValidationException(TransaccionMessageConstants.WrongMonto);

                var transaccion = NewTransaccion(transaccionDto);

                var numeroClave = masterRepository.TarjetaClave.FindByCondition(t =>
                    t.TarjetaClaveId == transaccion.TarjetaClaveId).First().NumeroClave;
                var transaccionResponseDto = new TransaccionResponseDtoOut()
                {
                    RowUiDTransaccion = transaccion.RowUid,
                    NumeroClave = numeroClave
                };

                masterRepository.Transaccion.Create(transaccion);
                masterRepository.Save();

                return ServiceResult<TransaccionResponseDtoOut>.ResultOk(transaccionResponseDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<TransaccionResponseDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<TransaccionResponseDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<bool> EjecutarTransaccion(Guid rowUidTransaccion, int clave)
        {
            try
            {
                if (!transaccionValidationService.IsExistingRowUid(rowUidTransaccion))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingRowUid);

                var transaccion = masterRepository.Transaccion.FindByCondition(t =>
                    t.RowUid == rowUidTransaccion).FirstOrDefault();

                var productoOrigen = masterRepository.Producto.FindByCondition(p =>
               p.ProductoId == transaccion.ProductoOrigenId).First();

                var productoDestino = masterRepository.Producto.FindByCondition(p =>
                    p.ProductoId == transaccion.ProductoDestinoId).First();

                ValidarTiposProductos(productoOrigen, productoDestino);


                if (transaccion.Estado == EstadoTransaccion.Rechazada)
                    throw new ValidationException(TransaccionMessageConstants.TransaccionRechazada + transaccion.Comentario);

                if (transaccion.Estado == EstadoTransaccion.Procesada)
                    throw new ValidationException(TransaccionMessageConstants.TransaccionProcesada);

                if (transaccion.Estado != EstadoTransaccion.Pendiente)
                    throw new ValidationException(TransaccionMessageConstants.EstadoTransaccionErroneo);

                TimeSpan span = (DateTime.Now - transaccion.FechaTransaccion);
                int minutosTransaccion = (int)span.TotalMinutes;

                if (minutosTransaccion >= 1)
                    CancelarTransaccion(transaccion, TransaccionMessageConstants.TiempoClaveAgotado);

                int clienteId = productoOrigen.TitularId;

                int claveRequerida = masterRepository.TarjetaClave.FindByCondition(t =>
                    t.TarjetaClaveId == transaccion.TarjetaClaveId && t.ClienteId == clienteId).FirstOrDefault().Clave;

                if (clave != claveRequerida)
                    throw new ValidationException(TransaccionMessageConstants.WrongClave);


                //Debito
                if (productoOrigen.TipoProducto == ((int)TipoProducto.CuentaAhorro))
                {
                    var cuentaAhorro = masterRepository.CuentaAhorro.FindByCondition(c =>
                    c.ProductoId == productoOrigen.ProductoId).First();

                    if (cuentaAhorro.Monto < transaccion.Monto)
                        CancelarTransaccion(transaccion, TransaccionMessageConstants.MontoInsuficiente);

                    cuentaAhorro.Monto -= transaccion.Monto;
                    masterRepository.CuentaAhorro.Update(cuentaAhorro);
                }
                else if (productoOrigen.TipoProducto == ((int)TipoProducto.TarjetaCredito))
                {
                    var tarjetaCredito = masterRepository.TarjetaCredito.FindByCondition(t =>
                        t.ProductoId == productoOrigen.ProductoId).FirstOrDefault();

                    if (tarjetaCredito.Balance < transaccion.Monto)
                        CancelarTransaccion(transaccion, TransaccionMessageConstants.MontoInsuficiente);

                    tarjetaCredito.Balance -= transaccion.Monto;
                    masterRepository.TarjetaCredito.Update(tarjetaCredito);
                }

                //Credito
                if (productoDestino.TipoProducto == ((int)TipoProducto.CuentaAhorro))
                {
                    var cuentaAhorro = masterRepository.CuentaAhorro.FindByCondition(c =>
                    c.ProductoId == productoDestino.ProductoId).FirstOrDefault();

                    cuentaAhorro.Monto += transaccion.Monto;
                    masterRepository.CuentaAhorro.Update(cuentaAhorro);
                }
                else if (productoDestino.TipoProducto == ((int)TipoProducto.TarjetaCredito))
                {
                    var tarjetaCredito = masterRepository.TarjetaCredito.FindByCondition(t =>
                        t.ProductoId == productoDestino.ProductoId).FirstOrDefault();

                    tarjetaCredito.Balance += transaccion.Monto;

                    if (tarjetaCredito.Balance > tarjetaCredito.LimiteCredito)
                        CancelarTransaccion(transaccion, TransaccionMessageConstants.PagoSobrepasaLimiteCredito);

                    if (transaccion.Monto >= tarjetaCredito.PagoMinimo)
                        tarjetaCredito.PagoMinimo = 0;

                    else
                        tarjetaCredito.PagoMinimo -= transaccion.Monto;

                    if (transaccion.Monto >= tarjetaCredito.PagoCorte)
                        tarjetaCredito.PagoCorte = 0;

                    else
                        tarjetaCredito.PagoCorte -= transaccion.Monto;

                    masterRepository.TarjetaCredito.Update(tarjetaCredito);
                }
                
                transaccion.Estado = EstadoTransaccion.Procesada;
                masterRepository.Transaccion.Update(transaccion);

                //Crear debito y debito de las transacciones
                var historialTransaccionDebitada = new HistorialTransaccion() {

                    TransaccionId = transaccion.TransaccionId,
                    //Transaccion = transaccion,

                    ClienteId = productoOrigen.TitularId,
                    //Cliente = masterRepository.Cliente.FindByCondition(c =>
                        //c.ClienteId == productoOrigen.TitularId).First(),

                    ProductoId = productoOrigen.ProductoId,
                    //Producto = productoOrigen,

                    Monto = transaccion.Monto,
                    TipoTransaccion = TipoTransaccion.Debito,
                    FechaTransaccion = DateTime.Now,
                    Descripcion = transaccion.Descripcion
                };

                var historialTransaccionAcreditada = new HistorialTransaccion()
                {
                    TransaccionId = transaccion.TransaccionId,
                    //Transaccion = transaccion,

                    ClienteId = productoDestino.TitularId,
                    //Cliente = masterRepository.Cliente.FindByCondition(c =>
                    //    c.ClienteId == productoDestino.TitularId).First(),

                    ProductoId = productoDestino.ProductoId,
                    //Producto = productoDestino,

                    Monto = transaccion.Monto,
                    TipoTransaccion = TipoTransaccion.Credito,
                    FechaTransaccion = DateTime.Now,
                    Descripcion = transaccion.Descripcion
                };

                masterRepository.HistorialTransaccion.Create(historialTransaccionDebitada);
                masterRepository.HistorialTransaccion.Create(historialTransaccionAcreditada);

                masterRepository.Save();

                return ServiceResult<bool>.ResultOk(true);
            }
            catch (ValidationException e)
            {
                return ServiceResult<bool>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        private string GetDescripcion(Producto productoOrigen, Producto productoDestino)
        {
            string descripcion;

            if (productoDestino.TipoProducto == ((int)TipoProducto.TarjetaCredito))
                descripcion = "Pago a tarjeta de credito";

            else if (productoOrigen.TitularId == productoDestino.TitularId)
                descripcion = "Transferencia entre cuentas";

            else
                descripcion = "Trasferencia a terceros";
            
            return descripcion;
        }

        private Transaccion NewTransaccion(TransaccionDtoIn transaccionDto)
        {
            var transaccion = mapper.Map<Transaccion>(transaccionDto);

            var productoOrigen = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == transaccionDto.ProductoOrigenId).First();

            var productoDestino = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == transaccionDto.ProductoDestinoId).First();

            ValidarTiposProductos(productoOrigen, productoDestino);

            //transaccion.ProductoOrigen = productoOrigen;
            //transaccion.ProductoDestino = productoDestino;

            transaccion.FechaTransaccion = DateTime.Now;
            transaccion.Estado = EstadoTransaccion.Pendiente;
            transaccion.Descripcion = GetDescripcion(productoOrigen, productoDestino);
            transaccion.RowUid = Guid.NewGuid();

            Random random = new Random();
            int numeroClave = random.Next(1, 10);
            int clienteId = productoOrigen.TitularId;

            var tarjetaClave = masterRepository.TarjetaClave.FindByCondition(t =>
                t.ClienteId == clienteId && t.NumeroClave == numeroClave).FirstOrDefault();

            transaccion.TarjetaClaveId = tarjetaClave.TarjetaClaveId;
            //transaccion.TarjetaClave = tarjetaClave;

            return transaccion;
        }

        private void ValidarTiposProductos(Producto productoOrigen, Producto productoDestino)
        {
            var tipoProducto = Convert.ToInt32(productoOrigen.TipoProducto);

            if (!productoValidationService.IsExistingTipoProducto(tipoProducto))
                throw new ValidationException(TransaccionMessageConstants.WrongTipoProductoOrigen);

            tipoProducto = Convert.ToInt32(productoDestino.TipoProducto);

            if (!productoValidationService.IsExistingTipoProducto(tipoProducto))
                throw new ValidationException(TransaccionMessageConstants.WrongTipoProductoDestino);
        }

        private void CancelarTransaccion(Transaccion transaccion, string mensaje)
        {
            transaccion.Comentario = mensaje;
            transaccion.Estado = EstadoTransaccion.Rechazada;
            masterRepository.Transaccion.Update(transaccion);
            masterRepository.Save();
            throw new ValidationException(TransaccionMessageConstants.TransaccionRechazada + transaccion.Comentario);
        }

    }
}
