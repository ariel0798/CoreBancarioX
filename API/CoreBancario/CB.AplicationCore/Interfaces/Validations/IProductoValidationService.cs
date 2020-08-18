
namespace CB.AplicationCore.Interfaces.Validations
{
    public interface IProductoValidationService
    {
        bool IsExistingProductoId(int productoId);
        bool IsExistingTipoProducto(int tipoProducto);
    }
}
