using System;
using AutoMapper;
using CB.AplicationCore.Constants;
using CB.AplicationCore.Interfaces;
using CB.AplicationCore.Interfaces.Validations;
using CB.Common.DTOs.DtoOut;
using CB.Common.Enums;
using CB.Common.Models;
using CB.Domain.Interfaces;
using CB.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CB.AplicationCore.Services
{
    public class CuentaAhorroService: ICuentaAhorroService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup 
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly ICuentaAhorroValidationService cuentaAhorroValidationService;

        public CuentaAhorroService(IMasterRepository masterRepository, IMapper mapper, 
            ICuentaAhorroValidationService cuentaAhorroValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.cuentaAhorroValidationService = cuentaAhorroValidationService;
        }

        private CuentaAhorroDtoOut MapCuentaAhorroToDto(CuentaAhorro cuentaAhorro)
        {
            var producto = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == cuentaAhorro.ProductoId).FirstOrDefault();

            var cuentaAhorroDto = mapper.Map<CuentaAhorroDtoOut>(cuentaAhorro);
            cuentaAhorroDto.Producto = mapper.Map<ProductoDtoOut>(producto);

            return cuentaAhorroDto;
        }

        public ServiceResult<CuentaAhorroDtoOut> GetCuentaAhorroByProductoId(int productoId)
        {
            try
            {
                if (!cuentaAhorroValidationService.IsExistingProductoId(productoId))
                    throw new ValidationException(CuentaAhorroMessageConstants.NotExistingProductoId);

                var cuentaAhorro = masterRepository.CuentaAhorro.FindByCondition(c =>
                     c.ProductoId == productoId).FirstOrDefault();

                var cuentaAhorroDto = MapCuentaAhorroToDto(cuentaAhorro);
                
                return ServiceResult<CuentaAhorroDtoOut>.ResultOk(cuentaAhorroDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<CuentaAhorroDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<CuentaAhorroDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
    }
}
