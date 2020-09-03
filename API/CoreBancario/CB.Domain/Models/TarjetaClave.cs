using System.Collections.Generic;

namespace CB.Domain.Models
{
    public class TarjetaClave
    {
        public TarjetaClave()
        {
            Transaccion = new HashSet<Transaccion>();
        }

        public int TarjetaClaveId { get; set; }
        public int ClienteId { get; set; }
        public int NumeroClave { get; set; }
        public int Clave { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Transaccion> Transaccion { get; set; }
    }
}
