using AutoMapper;
using CB.AplicationCore.Constants;
using CB.AplicationCore.Interfaces;
using CB.AplicationCore.Interfaces.Validations;
using CB.Common.DTOs.DtoOut;
using CB.Common.Enums;
using CB.Common.Models;
using CB.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CB.AplicationCore.Services
{
    public class ProductoService: IProductoService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly IProductoValidationService productoValidationService;
        readonly IClienteValidationService clienteValidationService;

        public ProductoService(IMasterRepository masterRepository, IMapper mapper, 
            IProductoValidationService productoValidationService,
            IClienteValidationService clienteValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.productoValidationService = productoValidationService;
            this.clienteValidationService = clienteValidationService;
        }
        public ServiceResult<ProductoDtoOut> GetProductoByProductoId(int productoId)
        {
            try
            {
                if (!productoValidationService.IsExistingProductoId(productoId))
                    throw new ValidationException(ProductoMessageConstants.NotExistingProductoId);

                var producto = masterRepository.Producto.FindByCondition(p =>
                    p.ProductoId == productoId).FirstOrDefault();

                var productoDto = mapper.Map<ProductoDtoOut>(producto);

                return ServiceResult<ProductoDtoOut>.ResultOk(productoDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<ProductoDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<ProductoDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }

        }

        public ServiceResult<List<ProductoDtoOut>> GetListProductosByClienteIdAndTipoProducto(int clienteId,int tipoProducto)
        {
            try
            {
                if (!clienteValidationService.IsExistingClienteId(clienteId))
                    throw new ValidationException(ClienteMessageConstants.NotExistingClienteId);

                if (!productoValidationService.IsExistingTipoProducto(tipoProducto))
                    throw new ValidationException(ProductoMessageConstants.NotExistingTipoProducto);

                var listProductos = masterRepository.Producto.FindByCondition(p =>
                    p.TipoProducto == tipoProducto && p.TitularId == clienteId);

                if (listProductos.Count() == 0)
                    throw new ValidationException(ProductoMessageConstants.NotProductsByParameters);

                var listProductosDto = new List<ProductoDtoOut>();

                foreach (var producto in listProductos)
                {
                    var productoDto = mapper.Map<ProductoDtoOut>(producto);
                    listProductosDto.Add(productoDto);
                }

                return ServiceResult<List<ProductoDtoOut>>.ResultOk(listProductosDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<List<ProductoDtoOut>>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<List<ProductoDtoOut>>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
    }
}
