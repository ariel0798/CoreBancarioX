using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class TarjetaCreditoProfile : Profile
    {
        public TarjetaCreditoProfile()
        {
            CreateMap<TarjetaCredito, TarjetaCreditoDtoOut>();
        }
    }
}
