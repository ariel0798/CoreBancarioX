﻿
namespace CB.Common.DTOs.DtoIn
{
    public class TransaccionDtoIn
    {
        public int ProductoOrigenId { get; set; }
        public int ProductoDestinoId { get; set; }
        public decimal Monto { get; set; }
        public string Nota { get; set; }
    }
}
