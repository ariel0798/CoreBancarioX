using System;
using System.Collections.Generic;

namespace CB.Domain.Models
{
    public partial class Transaccion
    {
        public Transaccion()
        {
            HistorialTransaccion = new HashSet<HistorialTransaccion>();
        }

        public int TransaccionId { get; set; }
        public int ProductoOrigenId { get; set; }
        public int ProductoDestinoId { get; set; }
        public int TarjetaClaveId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Nota { get; set; }
        public string Comentario { get; set; }
        public Guid RowUid { get; set; }

        public virtual Producto ProductoDestino { get; set; }
        public virtual Producto ProductoOrigen { get; set; }
        public virtual TarjetaClave TarjetaClave { get; set; }
        public virtual ICollection<HistorialTransaccion> HistorialTransaccion { get; set; }
    }
}
