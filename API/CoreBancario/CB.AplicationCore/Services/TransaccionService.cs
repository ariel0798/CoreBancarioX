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
using System.Collections.Generic;
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
        readonly IGeneralValidationService generalValidationService;
        readonly IProductoValidationService productoValidationService;

        public TransaccionService(IMasterRepository masterRepository, IMapper mapper, 
            ITransaccionValidationService transaccionValidationService, IClienteValidationService clienteValidationService,
            IProductoValidationService productoValidationService, IGeneralValidationService generalValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.transaccionValidationService = transaccionValidationService;
            this.generalValidationService = generalValidationService;
            this.productoValidationService = productoValidationService;
        }

        private TransaccionDtoOut MapTransaccionToDto(Transaccion transaccion)
        {
            var productoOrigen = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == transaccion.ProductoOrigenId).FirstOrDefault();

            var productoDestino = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == transaccion.ProductoDestinoId).FirstOrDefault();

            var transaccionDto = mapper.Map<TransaccionDtoOut>(transaccion);
            transaccionDto.ProductoOrigen = mapper.Map<ProductoDtoOut>(productoOrigen);
            transaccionDto.ProductoDestino = mapper.Map<ProductoDtoOut>(productoDestino);

            return transaccionDto;
        }

        public ServiceResult<TransaccionDtoOut> GetTransaccionByTransaccionId(int transaccionId)
        {
            try
            {
                if (!transaccionValidationService.IsExistingTransaccionId(transaccionId))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingTransaccionId);

                var transaccion = masterRepository.Transaccion.FindByCondition(t =>
                    t.TransaccionId == transaccionId).FirstOrDefault() ;

                var transaccionDto = MapTransaccionToDto(transaccion);

                return ServiceResult<TransaccionDtoOut>.ResultOk(transaccionDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<TransaccionDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<TransaccionDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<List<TransaccionDtoOut>> GetListTransaccionesByProductoOrigenId(int productoId)
        {
            try
            {
                if (!productoValidationService.IsExistingProductoId(productoId))
                    throw new ValidationException(ProductoMessageConstants.NotExistingProductoId);

                var listTransacciones = masterRepository.Transaccion.FindByCondition(t =>
                    t.ProductoOrigenId == productoId);

                if(listTransacciones.Count() == 0)
                    throw new ValidationException(TransaccionMessageConstants.NotExistingTransaccionesinProductoOrigen);

                var listTransaccionesDto = new List<TransaccionDtoOut>();

                foreach (var transaccion in listTransacciones)
                {
                    var transaccionDto = MapTransaccionToDto(transaccion);
                    listTransaccionesDto.Add(transaccionDto);
                }

                return ServiceResult<List<TransaccionDtoOut>>.ResultOk(listTransaccionesDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<List<TransaccionDtoOut>>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<List<TransaccionDtoOut>>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<bool> CreateTransaction(TransaccionDtoIn transaccionDto) 
        {
            try
            {
                if (!productoValidationService.IsExistingProductoId(transaccionDto.ProductoOrigenId))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingProductoOrigenId);

                if (!productoValidationService.IsExistingProductoId(transaccionDto.ProductoDestinoId))
                    throw new ValidationException(TransaccionMessageConstants.NotExistingProductoDestinoId);

                if (transaccionDto.Monto <= 0)
                    throw new ValidationException(TransaccionMessageConstants.WrongMonto);

                //validar los tipos de transaccion
                if (generalValidationService.IsEmptyText(transaccionDto.TipoTransaccion))
                    throw new ValidationException(TransaccionMessageConstants.EmptyTipoTransaccion);

                transaccionDto.TipoTransaccion = generalValidationService.GetRewrittenTextFirstCapitalLetter(transaccionDto.TipoTransaccion);

                //Proceso de cobro 

                var productoOrigen = masterRepository.Producto.FindByCondition(p =>
                    p.ProductoId == transaccionDto.ProductoOrigenId).First();

                var productoDestino = masterRepository.Producto.FindByCondition(p =>
                    p.ProductoId == transaccionDto.ProductoDestinoId).First();

                var tipoProducto = Convert.ToInt32(productoOrigen.TipoProducto);

                if (!productoValidationService.IsExistingTipoProducto(tipoProducto))
                    throw new ValidationException(TransaccionMessageConstants.WrongTipoProductoOrigen);

                tipoProducto = Convert.ToInt32(productoDestino.TipoProducto);

                if (!productoValidationService.IsExistingTipoProducto(tipoProducto))
                    throw new ValidationException(TransaccionMessageConstants.WrongTipoProductoDestino);

                //validar monto a cobrar por transaccion

                //debitar monto
                if(productoOrigen.TipoProducto == ((int)TipoProducto.CuentaAhorro))
                {
                    var cuentaAhorro = masterRepository.CuentaAhorro.FindByCondition(c => 
                    c.ProductoId == productoOrigen.ProductoId).First();

                    //Se crea registro de transaccion con fondo insuficiente?
                    if (cuentaAhorro.Monto < transaccionDto.Monto)
                        throw new ValidationException(TransaccionMessageConstants.MontoInsuficiente);

                    cuentaAhorro.Monto -= transaccionDto.Monto;
                    masterRepository.CuentaAhorro.Update(cuentaAhorro);
                }
                else if(productoOrigen.TipoProducto == ((int)TipoProducto.TarjetaCredito))
                {
                    var tarjetaCredito = masterRepository.TarjetaCredito.FindByCondition(t =>
                        t.ProductoId == productoOrigen.ProductoId).FirstOrDefault();

                    if(tarjetaCredito.Balance < transaccionDto.Monto)
                        throw new ValidationException(TransaccionMessageConstants.MontoInsuficiente);

                    tarjetaCredito.Balance -= transaccionDto.Monto;
                    masterRepository.TarjetaCredito.Update(tarjetaCredito);
                }

                //acreditar monto
                if (productoDestino.TipoProducto == ((int)TipoProducto.CuentaAhorro))
                {
                    var cuentaAhorro = masterRepository.CuentaAhorro.FindByCondition(c =>
                    c.ProductoId == productoDestino.ProductoId).FirstOrDefault();

                    cuentaAhorro.Monto += transaccionDto.Monto;
                    masterRepository.CuentaAhorro.Update(cuentaAhorro);
                }
                else if (productoDestino.TipoProducto == ((int)TipoProducto.TarjetaCredito))
                {
                    var tarjetaCredito = masterRepository.TarjetaCredito.FindByCondition(t =>
                        t.ProductoId == productoDestino.ProductoId).FirstOrDefault();

                    tarjetaCredito.Balance += transaccionDto.Monto;
                    masterRepository.TarjetaCredito.Update(tarjetaCredito); 
                }

                var transaccion = mapper.Map<Transaccion>(transaccionDto);

                transaccion.FechaTransaccion = DateTime.Now;
                transaccion.Estado = "Procesada";
                /*
                transaccion.ProductoOrigen = productoOrigen;
                transaccion.ProductoDestino = productoDestino;
                */
                masterRepository.Transaccion.Create(transaccion);
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

        
    }
}
