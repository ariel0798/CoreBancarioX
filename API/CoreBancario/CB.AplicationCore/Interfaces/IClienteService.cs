using CB.Common.DTOs.DtoOut;
using CB.Common.Models;

namespace CB.AplicationCore.Interfaces
{
    public interface IClienteService
    {
        ServiceResult<ClienteDtoOut> GetClienteByClienteId(int clienteId);
        ServiceResult<ClienteDtoOut> GetClienteByCedula(string cedula);
    }
}
