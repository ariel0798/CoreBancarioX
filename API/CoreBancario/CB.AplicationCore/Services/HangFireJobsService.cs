using CB.AplicationCore.Constants;
using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using CB.Common.Models;
using CB.Domain.Interfaces;
using CB.Domain.Models;
using System;
using System.Linq;

namespace CB.AplicationCore.Services
{
    public class HangFireJobsService: IHangFireJobsService
    {
        readonly IMasterRepository masterRepository;

        public HangFireJobsService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        private const decimal InteresMensual = 0.025M;
        private const decimal PorcentajePagoMinimo = 0.02M;
        private const int DiasLimiteRealizarPago = 21;

        public ServiceResult<bool> GenerarCargoInteresTarjetaAndGenerarCorte()
        {
            try
            {
                var listTarjetasCreditoNoCorte = masterRepository.TarjetaCredito.FindByCondition(t =>
                    t.FechaCorte.Day != DateTime.Now.Day);

                foreach (var tarjetaCreditoNoCorte in listTarjetasCreditoNoCorte)
                {

                    if (tarjetaCreditoNoCorte.DiasLimitePago == 1)
                    {
                        tarjetaCreditoNoCorte.DiasLimitePago = 0;

                        decimal PagoCorte = tarjetaCreditoNoCorte.PagoCorte;
                        
                        if(PagoCorte > 0)
                        {
                            decimal cargoPorNoPago = PagoCorte * InteresMensual;
                            tarjetaCreditoNoCorte.Balance -= cargoPorNoPago;
                            tarjetaCreditoNoCorte.PagoCorte = 0;
                            tarjetaCreditoNoCorte.PagoMinimo = 0;

                            masterRepository.TarjetaCredito.Update(tarjetaCreditoNoCorte);

                            var producto = masterRepository.Producto.FindByCondition(p =>
                                p.ProductoId == tarjetaCreditoNoCorte.ProductoId).First();

                            var historialTransaccionDebitada = new HistorialTransaccion()
                            {
                                ClienteId = producto.TitularId,

                                ProductoId = producto.ProductoId,

                                Monto = cargoPorNoPago,
                                TipoTransaccion = TipoTransaccion.Debito,
                                FechaTransaccion = DateTime.Now,
                                Descripcion = TransaccionMessageConstants.CargoInteresPorNoPago
                            };

                            masterRepository.HistorialTransaccion.Create(historialTransaccionDebitada);

                        }
                        
                    }
                    else if(tarjetaCreditoNoCorte.DiasLimitePago > 1)
                    {
                        tarjetaCreditoNoCorte.DiasLimitePago--;
                        masterRepository.TarjetaCredito.Update(tarjetaCreditoNoCorte);
                       
                    }
                }
                
                var listTarjetasCreditoCorte = masterRepository.TarjetaCredito.FindByCondition(t =>
                    t.FechaCorte.Day == DateTime.Now.Day);

                foreach (var tarjetaCreditoCorte in listTarjetasCreditoCorte)
                {
                    decimal limiteCredito = tarjetaCreditoCorte.LimiteCredito,
                        balance = tarjetaCreditoCorte.Balance;
                    
                    tarjetaCreditoCorte.DiasLimitePago = DiasLimiteRealizarPago;
                    tarjetaCreditoCorte.PagoCorte = limiteCredito - balance;
                    tarjetaCreditoCorte.PagoMinimo = tarjetaCreditoCorte.PagoCorte * PorcentajePagoMinimo;
                    masterRepository.TarjetaCredito.Update(tarjetaCreditoCorte);
                   
                }
                masterRepository.Save();
                return ServiceResult<bool>.ResultOk(true);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.ResultFailed(ResponseCode.Error,e.Message);
            }
        }

        public ServiceResult<bool> EliminarTransaccionesPendientes()
        {
            try
            {
                var listTransaccionesPendientes = masterRepository.Transaccion.FindByCondition(t => 
                        t.Estado == EstadoTransaccion.Pendiente);

                foreach (var transaccionPendiente in listTransaccionesPendientes)
                {
                    TimeSpan span = (DateTime.Now - transaccionPendiente.FechaTransaccion);
                    int minutosTransaccion = (int)span.TotalMinutes;

                    if (minutosTransaccion >= 1)
                    {
                        transaccionPendiente.Comentario = TransaccionMessageConstants.TiempoClaveAgotado;
                        transaccionPendiente.Estado = EstadoTransaccion.Rechazada;
                        masterRepository.Transaccion.Update(transaccionPendiente);
                    }
                }
                masterRepository.Save();
                return ServiceResult<bool>.ResultOk(true);
            }
            catch (Exception e)
            {
                return ServiceResult<bool>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
    }
}
