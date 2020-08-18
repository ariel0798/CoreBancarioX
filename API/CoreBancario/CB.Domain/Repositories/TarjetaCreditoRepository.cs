using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class TarjetaCreditoRepository : BaseRepository<TarjetaCredito>, ITarjetaCreditoRepository
    {
        public TarjetaCreditoRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
