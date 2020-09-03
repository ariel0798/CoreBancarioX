using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class HistorialTransaccionValidationService: IHistorialTransaccionValidationService
    {
        readonly IMasterRepository masterRepository;

        public HistorialTransaccionValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingHistorialTransaccionId(int historialTransaccionId)
        {
            var isExisting = masterRepository.HistorialTransaccion.GetAll().Any(h =>
               h.HistorialTransaccionId == historialTransaccionId);

            return isExisting;
        }
    }
}
