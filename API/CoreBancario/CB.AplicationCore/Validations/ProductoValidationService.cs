using CB.AplicationCore.Interfaces.Validations;
using CB.Common.Enums;
using CB.Domain.Interfaces;
using System;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class ProductoValidationService: IProductoValidationService
    {
        readonly IMasterRepository masterRepository;

        public ProductoValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingProductoId(int productoId)
        {
            var isExisting = masterRepository.Producto.GetAll().Any(c =>
                c.ProductoId == productoId);

            return isExisting;
        }

        public bool IsExistingTipoProducto(int tipoProducto)
        {
            var isExisting = Enum.IsDefined(typeof(TipoProducto), tipoProducto);
            return isExisting;
        }

    }
}
