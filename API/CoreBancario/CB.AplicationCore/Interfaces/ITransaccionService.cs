using CB.Common.DTOs.DtoIn;
using CB.Common.DTOs.DtoOut;
using CB.Common.Models;
using System;

namespace CB.AplicationCore.Interfaces
{
    public interface ITransaccionService
    {
        ServiceResult<TransaccionResponseDtoOut> CreateTransaction(TransaccionDtoIn transaccionDto);
        ServiceResult<bool> EjecutarTransaccion(Guid rowUidTransaccion, int clave);
    }
}
