using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class HistorialTransaccionRepository : BaseRepository<HistorialTransaccion>, IHistorialTransaccionRepository
    {
        public HistorialTransaccionRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
