using CB.Common.DTOs.DtoIn;
using CB.Common.DTOs.DtoOut;
using CB.Common.Models;
using System.Collections.Generic;

namespace CB.AplicationCore.Interfaces
{
    public interface ITransaccionService
    {
        ServiceResult<TransaccionDtoOut> GetTransaccionByTransaccionId(int transaccionId);
        ServiceResult<List<TransaccionDtoOut>> GetListTransaccionesByProductoOrigenId(int productoId);

        ServiceResult<bool> CreateTransaction(TransaccionDtoIn transaccionDto);
    }
}
