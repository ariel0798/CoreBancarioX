using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class CuentaAhorroProfile : Profile
    {
        public CuentaAhorroProfile()
        {
            CreateMap<CuentaAhorro, CuentaAhorroDtoOut>();
        }
    }
}
