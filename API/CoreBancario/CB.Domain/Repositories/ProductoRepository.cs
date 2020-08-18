using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
