using System;

namespace CB.AplicationCore.Interfaces.Validations
{
    public interface ITransaccionValidationService
    {
        bool IsExistingTransaccionId(int transccionId);
        bool IsExistingRowUid(Guid rowUid);
    }
}
