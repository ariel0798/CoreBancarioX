using CB.Common.DTOs.DtoOut;
using CB.Common.Models;
using System.Collections.Generic;

namespace CB.AplicationCore.Interfaces
{
    public interface IHistorialTransaccionService
    {
        ServiceResult<HistorialTransaccionDtoOut> GetHistorialTransaccionByHistorialTransaccionId(int historialTransaccionId);
        ServiceResult<List<HistorialTransaccionDtoOut>> GetListHistorialTransaccionesByProductoId(int productoId);
    }
}
