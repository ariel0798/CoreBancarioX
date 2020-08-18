using System;

namespace CB.Domain.Models
{
    public partial class Transaccion
    {
        public int TransaccionId { get; set; }
        public int ProductoOrigenId { get; set; }
        public int ProductoDestinoId { get; set; }
        public decimal Monto { get; set; }
        public string TipoTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string Estado { get; set; }
        public string Descripccion { get; set; }

        public virtual Producto ProductoDestino { get; set; }
        public virtual Producto ProductoOrigen { get; set; }
    }
}
