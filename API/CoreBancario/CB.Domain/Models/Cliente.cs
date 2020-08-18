using System.Collections.Generic;

namespace CB.Domain.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Producto = new HashSet<Producto>();
        }

        public int ClienteId { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
