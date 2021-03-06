﻿using System.Collections.Generic;

namespace CB.Common.DTOs.DtoOut
{
    public class ClienteDtoOut
    {
        public int ClienteId { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public List<ProductoDtoOut> Productos { get; set; }
    }
}
