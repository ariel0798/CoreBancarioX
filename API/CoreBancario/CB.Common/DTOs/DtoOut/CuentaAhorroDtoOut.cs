
namespace CB.Common.DTOs.DtoOut
{
    public class CuentaAhorroDtoOut
    {
        public ProductoDtoOut Producto { get; set; }
        public string TipoMoneda { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
    }
}
