using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class TarjetaClaveRepository : BaseRepository<TarjetaClave>, ITarjetaClaveRepository
    {
        public TarjetaClaveRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
