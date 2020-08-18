using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class CuentaAhorroValidationService: ICuentaAhorroValidationService
    {
        readonly IMasterRepository masterRepository;

        public CuentaAhorroValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingProductoId(int productoId)
        {
            var isExisting = masterRepository.CuentaAhorro.GetAll().Any(c =>
                c.ProductoId == productoId);

            return isExisting;
        }
    }
}
