using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
using System;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class TransaccionValidationService: ITransaccionValidationService
    {
        readonly IMasterRepository masterRepository;

        public TransaccionValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingTransaccionId(int transccionId)
        {
            var isExisting = masterRepository.Transaccion.GetAll().Any(t => 
                t.TransaccionId == transccionId);

            return isExisting;
        
        }

        public bool IsExistingRowUid(Guid rowUid)
        {
            var isExisting = masterRepository.Transaccion.GetAll().Any(t =>
                t.RowUid == rowUid);

            return isExisting;

        }

    }
}
