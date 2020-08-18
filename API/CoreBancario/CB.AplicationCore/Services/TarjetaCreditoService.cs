using AutoMapper;
using CB.AplicationCore.Constants;
using CB.AplicationCore.Interfaces;
using CB.AplicationCore.Interfaces.Validations;
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
    public class TarjetaCreditoService: ITarjetaCreditoService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup TarjetaCreditoMessageConstants
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly ITarjetaCreditoValidationService tarjetaCreditoValidationService;

        public TarjetaCreditoService(IMasterRepository masterRepository, IMapper mapper, 
            ITarjetaCreditoValidationService tarjetaCreditoValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.tarjetaCreditoValidationService = tarjetaCreditoValidationService;
        }

        private TarjetaCreditoDtoOut MapTarjetaCreditoToDto(TarjetaCredito tarjetaCredito)
        {
            var producto = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == tarjetaCredito.ProductoId).FirstOrDefault();

            var tarjetaCreditoDto = mapper.Map<TarjetaCreditoDtoOut>(tarjetaCredito);
            tarjetaCreditoDto.Producto = mapper.Map<ProductoDtoOut>(producto);

            return tarjetaCreditoDto;
        }


        public ServiceResult<TarjetaCreditoDtoOut> GetTarjetaCreditoByProductoId(int productoId)
        {
            try
            {
                if (!tarjetaCreditoValidationService.IsExistingProductoId(productoId))
                    throw new ValidationException(TarjetaCreditoMessageConstants.NotExistingProductoId);

                var tarjetaCredito = masterRepository.TarjetaCredito.FindByCondition(t =>
                     t.ProductoId == productoId).FirstOrDefault();

                var tarjetaCreditoDto = MapTarjetaCreditoToDto(tarjetaCredito);

                return ServiceResult<TarjetaCreditoDtoOut>.ResultOk(tarjetaCreditoDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<TarjetaCreditoDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<TarjetaCreditoDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
    }
}
