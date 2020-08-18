

namespace CB.Domain.Models
{
    public partial class CuentaAhorro
    {
        public int ProductoId { get; set; }
        public string TipoMoneda { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }

    }
}
