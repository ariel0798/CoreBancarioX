
namespace CB.Common.DTOs.DtoOut
{
    public class BeneficiarioDtoOut
    {
        public int ClienteId { get; set; }
        public int ClienteBeneficiarioId { get; set; }
        public ProductoDtoOut Producto { get; set; }
        public string Alias { get; set; }
    }
}
