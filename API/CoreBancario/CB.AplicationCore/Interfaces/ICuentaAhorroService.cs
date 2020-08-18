using CB.Common.DTOs.DtoOut;
using CB.Common.Models;

namespace CB.AplicationCore.Interfaces
{
    public interface ICuentaAhorroService
    {
        ServiceResult<CuentaAhorroDtoOut> GetCuentaAhorroByProductoId(int productoId);
    }
}
