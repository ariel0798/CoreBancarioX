using CB.Common.Models;

namespace CB.AplicationCore.Interfaces
{
    public interface IHangFireJobsService
    {
        ServiceResult<bool> GenerarCargoInteresTarjetaAndGenerarCorte();
        ServiceResult<bool> EliminarTransaccionesPendientes();
    }
}
