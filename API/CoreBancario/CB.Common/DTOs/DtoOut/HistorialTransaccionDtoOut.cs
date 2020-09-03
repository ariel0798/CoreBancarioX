using System;

namespace CB.Common.DTOs.DtoOut
{
    public class HistorialTransaccionDtoOut
    {
        public int HistorialTransaccionId { get; set; }
        public int TransaccionId { get; set; }
        public int ClienteId { get; set; }
        public ProductoDtoOut Producto { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public string Descripcion { get; set; }
    }
}
