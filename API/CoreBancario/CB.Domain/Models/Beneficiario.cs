
namespace CB.Domain.Models
{
    public partial class Beneficiario
    {
        public int BeneficiarioId { get; set; }
        public int ClienteId { get; set; }
        public int ClienteBeneficiarioId { get; set; }
        public int ClienteBeneficiarioProductoId { get; set; }
        public string Alias { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Cliente ClienteBeneficiario { get; set; }
        public virtual Producto ClienteBeneficiarioProducto { get; set; }
    }
}
