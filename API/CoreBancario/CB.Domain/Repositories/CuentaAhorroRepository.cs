using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class CuentaAhorroRepository : BaseRepository<CuentaAhorro>, ICuentaAhorroRepository
    {
        public CuentaAhorroRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
