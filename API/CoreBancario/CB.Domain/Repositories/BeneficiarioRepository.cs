using CB.Domain.Context;
using CB.Domain.Interfaces;
using CB.Domain.Models;

namespace CB.Domain.Repositories
{
    public class BeneficiarioRepository : BaseRepository<Beneficiario>, IBeneficiarioRepository
    {
        public BeneficiarioRepository(CoreBancoDbContext context) : base(context)
        {
        }
    }
}
