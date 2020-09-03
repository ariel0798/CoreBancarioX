using CB.Common.DTOs.DtoOut;
using CB.Common.Models;
using System.Collections.Generic;

namespace CB.AplicationCore.Interfaces
{
    public interface IBeneficiarioService
    {
        ServiceResult<List<BeneficiarioDtoOut>> GetListBeneficiariosByClienteId(int clienteId);
        ServiceResult<BeneficiarioDtoOut> GetBeneficiarioByClienteBeneficiarioId(int clienteBeneficiarioId);
    }
}
