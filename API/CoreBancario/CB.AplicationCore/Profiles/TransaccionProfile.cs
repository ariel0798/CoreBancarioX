using AutoMapper;
using CB.Common.DTOs.DtoIn;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class TransaccionProfile : Profile
    {
        public TransaccionProfile()
        {
            CreateMap<Transaccion, TransaccionDtoOut>();
            CreateMap<TransaccionDtoIn, Transaccion>();
        }
    }
}
