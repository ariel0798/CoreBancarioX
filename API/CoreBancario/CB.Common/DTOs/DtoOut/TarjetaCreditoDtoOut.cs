using System;

namespace CB.Common.DTOs.DtoOut
{
    public class TarjetaCreditoDtoOut
    {
        public ProductoDtoOut Producto { get; set; }
        public DateTime FechaCorte { get; set; }
        public int DiasLimitePago { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Balance { get; set; }
        public decimal PagoMinimo { get; set; }
        public decimal PagoCorte { get; set; }
        public string Estado { get; set; }

    }
}
