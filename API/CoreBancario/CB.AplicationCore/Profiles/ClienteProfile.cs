using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDtoOut>();
        }
    }
}
