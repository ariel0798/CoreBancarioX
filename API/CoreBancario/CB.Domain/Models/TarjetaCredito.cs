using System;

namespace CB.Domain.Models
{
    public partial class TarjetaCredito
    {
        public int TarjetaCreditoId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaCorte { get; set; }
        public int DiasLimitePago { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Balance { get; set; }
        public decimal PagoMinimo { get; set; }
        public decimal PagoCorte { get; set; }
        public string Estado { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
