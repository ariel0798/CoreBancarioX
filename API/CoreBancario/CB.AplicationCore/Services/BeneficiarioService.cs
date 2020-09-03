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

namespace CB.AplicationCore.Services
{
    public class BeneficiarioService: IBeneficiarioService
    {
        //Clase e interfaz de validacion , interfaz de esta clase, DTOs,  mapper, mensajes constantes, dependecy intejection en startup
        readonly IMasterRepository masterRepository;
        readonly IMapper mapper;
        readonly IGeneralValidationService generalValidationService;
        readonly IClienteValidationService clienteValidationService;

        public BeneficiarioService(IMasterRepository masterRepository, IMapper mapper, 
            IGeneralValidationService generalValidationService, IClienteValidationService clienteValidationService)
        {
            this.masterRepository = masterRepository;
            this.mapper = mapper;
            this.generalValidationService = generalValidationService;
            this.clienteValidationService = clienteValidationService;
        }

        private BeneficiarioDtoOut MapBeneficiarioToDto(Beneficiario beneficiario)
        {
            var producto = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == beneficiario.ClienteBeneficiarioProductoId).FirstOrDefault();

            var beneficiarioDto = mapper.Map<BeneficiarioDtoOut>(beneficiario);

            beneficiarioDto.Producto = mapper.Map<ProductoDtoOut>(producto);

            return beneficiarioDto;
        }

        public ServiceResult<List<BeneficiarioDtoOut>> GetListBeneficiariosByClienteId(int clienteId)
        {
            try
            {
                if (!clienteValidationService.IsExistingClienteId(clienteId))
                    throw new ValidationException(ClienteMessageConstants.NotExistingClienteId);

                var listBeneficiarios= masterRepository.Beneficiario.FindByCondition(b =>
                    b.ClienteId == clienteId);

                if (listBeneficiarios.Count() == 0)
                    throw new ValidationException(BeneficiarioMessageConstants.NotExistingBeneficiarios);

                var listBeneficiariosDto = new List<BeneficiarioDtoOut>();

                foreach (var beneficiario in listBeneficiarios)
                {
                    var beneficiarioDto = MapBeneficiarioToDto(beneficiario);
                    listBeneficiariosDto.Add(beneficiarioDto);
                }

                return ServiceResult<List<BeneficiarioDtoOut>>.ResultOk(listBeneficiariosDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<List<BeneficiarioDtoOut>>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<List<BeneficiarioDtoOut>>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

        public ServiceResult<BeneficiarioDtoOut> GetBeneficiarioByClienteBeneficiarioId(int clienteBeneficiarioId)
        {
            try
            {
                if (!clienteValidationService.IsExistingClienteId(clienteBeneficiarioId))
                    throw new ValidationException(BeneficiarioMessageConstants.NotExistingBeneficiarioId);

                var beneficiario = masterRepository.Beneficiario.FindByCondition(b =>
                     b.ClienteBeneficiarioId == clienteBeneficiarioId).FirstOrDefault();

                if(beneficiario == null)
                    throw new ValidationException(BeneficiarioMessageConstants.NotExistingBeneficiarioId);

                var beneficiarioDto = MapBeneficiarioToDto(beneficiario);

                return ServiceResult<BeneficiarioDtoOut>.ResultOk(beneficiarioDto);
            }
            catch (ValidationException e)
            {
                return ServiceResult<BeneficiarioDtoOut>.ResultFailed(ResponseCode.Warning, e.Message);
            }
            catch (Exception e)
            {
                return ServiceResult<BeneficiarioDtoOut>.ResultFailed(ResponseCode.Error, e.Message);
            }
        }

    }
}
