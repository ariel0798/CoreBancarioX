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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CB.AplicationCore.Services
{
    public class HistorialTransaccionService: IHistorialTransaccionService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup 
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly IHistorialTransaccionValidationService historialTransaccionValidationService;
        readonly IProductoValidationService productoValidationService;

        public HistorialTransaccionService(IMasterRepository masterRepository, IMapper mapper, 
            IHistorialTransaccionValidationService historialTransaccionValidationService, 
            IProductoValidationService productoValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.historialTransaccionValidationService = historialTransaccionValidationService;
            this.productoValidationService = productoValidationService;
        }

        private HistorialTransaccionDtoOut MapToDto(HistorialTransaccion historialTransaccion)
        {
            var producto = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == historialTransaccion.ProductoId).FirstOrDefault();

            var historialTransaccionDto = mapper.Map<HistorialTransaccionDtoOut>(historialTransaccion);

            historialTransaccionDto.Producto = mapper.Map<ProductoDtoOut>(producto);

            return historialTransaccionDto;
        }
        public ServiceResult<HistorialTransaccionDtoOut> GetHistorialTransaccionByHistorialTransaccionId(int historialTransaccionId)
        {
            try
            {
                if (!historialTransaccionValidationService.IsExistingHistorialTransaccionId(historialTransaccionId))
                    throw new ValidationException(HistorialTransaccionMessageConstants.NotExistingHistorialTransaccionId);

                var historialTransaccion = masterRepository.HistorialTransaccion.FindByCondition(h =>
                    h.HistorialTransaccionId == historialTransaccionId).First();

                var historialTransaccionDto = MapToDto(historialTransaccion);

                return ServiceResult<HistorialTransaccionDtoOut>.ResultOk(historialTransaccionDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<HistorialTransaccionDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<HistorialTransaccionDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<List<HistorialTransaccionDtoOut>> GetListHistorialTransaccionesByProductoId(int productoId)
        {
            try
            {
                if (!productoValidationService.IsExistingProductoId(productoId))
                    throw new ValidationException(ProductoMessageConstants.NotExistingProductoId);

                var listHistorialTransacciones = masterRepository.HistorialTransaccion.FindByCondition(h =>
                    h.ProductoId == productoId);

                if (listHistorialTransacciones.Count() == 0)
                    throw new ValidationException(HistorialTransaccionMessageConstants.NotProductsByParameters);

                var listHistorialTransaccionesDto = new List<HistorialTransaccionDtoOut>();

                foreach (var historialTransaccion in listHistorialTransacciones)
                {
                    var historialTransaccionDto = MapToDto(historialTransaccion);
                    listHistorialTransaccionesDto.Add(historialTransaccionDto);
                }

                return ServiceResult<List<HistorialTransaccionDtoOut>>.ResultOk(listHistorialTransaccionesDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<List<HistorialTransaccionDtoOut>>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<List<HistorialTransaccionDtoOut>>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
    }
}
