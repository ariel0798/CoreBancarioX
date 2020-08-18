using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class TransaccionRepository : BaseRepository<Transaccion>, ITransaccionRepository
    {
        public TransaccionRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
