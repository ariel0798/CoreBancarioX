using CB.Common.DTOs.DtoOut;
using CB.Common.Models;
using System.Collections.Generic;

namespace CB.AplicationCore.Interfaces
{
    public interface IProductoService
    {
        ServiceResult<ProductoDtoOut> GetProductoByProductoId(int productoId);
        ServiceResult<List<ProductoDtoOut>> GetListProductosByClienteIdAndTipoProducto(int clienteId, int tipoProducto);
    }
}
