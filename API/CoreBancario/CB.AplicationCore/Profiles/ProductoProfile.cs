using AutoMapper;
using CB.Common.DTOs.DtoOut;
using CB.Common.Enums;
using CB.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CB.AplicationCore.Profiles
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Producto, ProductoDtoOut>()
                .ForMember(p => p.TipoProducto, o => o.MapFrom(x => ((TipoProducto)Convert.ToInt32(x.TipoProducto)).ToString()));
        }
    }
}
