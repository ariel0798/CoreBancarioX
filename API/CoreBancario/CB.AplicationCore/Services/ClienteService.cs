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
    public class ClienteService: IClienteService
    {
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly IGeneralValidationService generalValidationService;
        readonly IClienteValidationService clienteValidationService;

        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup
        public ClienteService(IMasterRepository masterRepository, IMapper mapper,
            IGeneralValidationService generalValidationService, IClienteValidationService clienteValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.generalValidationService = generalValidationService;
            this.clienteValidationService = clienteValidationService;
        }

        private ClienteDtoOut MapClienteToDto(Cliente cliente)
        {
            var clienteDto = mapper.Map<ClienteDtoOut>(cliente);

            var listProductos = masterRepository.Producto.FindByCondition(p =>
                p.TitularId == cliente.ClienteId);

            var listProductosDto = new List<ProductoDtoOut>();

            foreach (var producto in listProductos)
            {
                var productoDto = mapper.Map<ProductoDtoOut>(producto);
                listProductosDto.Add(productoDto);
            }

            clienteDto.Productos = listProductosDto;

            return clienteDto;
        }
        public ServiceResult<ClienteDtoOut> GetClienteByClienteId(int clienteId)
        {
            try
            {
                if (!clienteValidationService.IsExistingClienteId(clienteId))
                    throw new ValidationException(ClienteMessageConstants.NotExistingClienteId);

                var cliente = masterRepository.Cliente.FindByCondition(c => 
                    c.ClienteId == clienteId).FirstOrDefault();

                var clienteDto = MapClienteToDto(cliente);

                return ServiceResult<ClienteDtoOut>.ResultOk(clienteDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<ClienteDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<ClienteDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<ClienteDtoOut> GetClienteByCedula(string cedula)
        {
            try
            {
                if (!clienteValidationService.IsExistingCedula(cedula))
                    throw new ValidationException(ClienteMessageConstants.NotExistingCedula);

                var cliente = masterRepository.Cliente.FindByCondition(c =>
                    c.Cedula == cedula).FirstOrDefault();

                var clienteDto = MapClienteToDto(cliente);

                return ServiceResult<ClienteDtoOut>.ResultOk(clienteDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<ClienteDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<ClienteDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }
        
    }
}
