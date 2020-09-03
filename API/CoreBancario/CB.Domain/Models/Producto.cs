using System.Collections.Generic;

namespace CB.Domain.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Beneficiario = new HashSet<Beneficiario>();
            CuentaAhorro = new HashSet<CuentaAhorro>();
            HistorialTransaccion = new HashSet<HistorialTransaccion>();
            TarjetaCredito = new HashSet<TarjetaCredito>();
            TransaccionProductoDestino = new HashSet<Transaccion>();
            TransaccionProductoOrigen = new HashSet<Transaccion>();
        }

        public int ProductoId { get; set; }
        public int TitularId { get; set; }
        public int TipoProducto { get; set; }
        public long NumeroProducto { get; set; }
        public string Alias { get; set; }

        public virtual Cliente Titular { get; set; }
        public virtual ICollection<Beneficiario> Beneficiario { get; set; }
        public virtual ICollection<CuentaAhorro> CuentaAhorro { get; set; }
        public virtual ICollection<HistorialTransaccion> HistorialTransaccion { get; set; }
        public virtual ICollection<TarjetaCredito> TarjetaCredito { get; set; }
        public virtual ICollection<Transaccion> TransaccionProductoDestino { get; set; }
        public virtual ICollection<Transaccion> TransaccionProductoOrigen { get; set; }
    }
}
