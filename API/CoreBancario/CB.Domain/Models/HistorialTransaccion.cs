using System;

namespace CB.Domain.Models
{
    public class HistorialTransaccion
    {
        public int HistorialTransaccionId { get; set; }
        public int TransaccionId { get; set; }
        public int ClienteId { get; set; }
        public int ProductoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string TipoTransaccion { get; set; }
        public string Descripcion { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Transaccion Transaccion { get; set; }
    }
}
