using CB.Common.DTOs.DtoOut;
using CB.Common.Models;

namespace CB.AplicationCore.Interfaces
{
    public interface ITarjetaCreditoService
    {
        ServiceResult<TarjetaCreditoDtoOut> GetTarjetaCreditoByProductoId(int productoId);
    }
}
