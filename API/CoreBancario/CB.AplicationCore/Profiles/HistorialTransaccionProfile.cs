using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Domain.Models;

namespace CB.AplicationCore.Profiles
{
    public class HistorialTransaccionProfile:Profile
    {
        public HistorialTransaccionProfile()
        {
            CreateMap<HistorialTransaccion, HistorialTransaccionDtoOut>();
        }
    }
}
