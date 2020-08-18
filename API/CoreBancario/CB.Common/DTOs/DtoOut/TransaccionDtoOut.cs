using System;

namespace CB.Common.DTOs.DtoOut
{
    public class TransaccionDtoOut
    {
        public int TransaccionId { get; set; }
        public ProductoDtoOut ProductoOrigen { get; set; }
        public ProductoDtoOut ProductoDestino { get; set; }
        public decimal Monto { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string Estado { get; set; }
        public string Descripccion { get; set; }
    }
}
