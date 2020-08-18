using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class TarjetaCreditoValidationService: ITarjetaCreditoValidationService
    {
        readonly IMasterRepository masterRepository;

        public TarjetaCreditoValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingProductoId(int productoId)
        {
            var isExisting = masterRepository.TarjetaCredito.GetAll().Any(t =>
                t.ProductoId == productoId);

            return isExisting;
        }
    }
}
