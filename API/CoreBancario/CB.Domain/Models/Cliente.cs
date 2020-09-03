using System.Collections.Generic;

namespace CB.Domain.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            BeneficiarioCliente = new HashSet<Beneficiario>();
            BeneficiarioClienteBeneficiario = new HashSet<Beneficiario>();
            HistorialTransaccion = new HashSet<HistorialTransaccion>();
            Producto = new HashSet<Producto>();
            TarjetaClave = new HashSet<TarjetaClave>();
        }

        public int ClienteId { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Beneficiario> BeneficiarioCliente { get; set; }
        public virtual ICollection<Beneficiario> BeneficiarioClienteBeneficiario { get; set; }
        public virtual ICollection<HistorialTransaccion> HistorialTransaccion { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<TarjetaClave> TarjetaClave { get; set; }
    }
}
