using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class BeneficiarioProfile : Profile
    {
        public BeneficiarioProfile()
        {
            CreateMap<Beneficiario, BeneficiarioDtoOut>();
        }
    }
}
