using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
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
        /*
        public bool HasEnoughtMontoDisponible(int productoId,decimal monto)
        {
            isExisting
            var producto = masterRepository.Producto.FindByCondition(p =>
                p.ProductoId == productoId).First();

            //Como saber que producto es para mirarle el monto
            if(producto.TipoProducto == "1")
            {

            }
            

            return isExisting;
        }
        */
    }
}
