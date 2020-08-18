using CB.AplicationCore.Interfaces.Validations;
using CB.Domain.Interfaces;
using System.Linq;

namespace CB.AplicationCore.Validations
{
    public class ClienteValidationService: IClienteValidationService
    {
        readonly IMasterRepository masterRepository;

        public ClienteValidationService(IMasterRepository masterRepository)
        {
            this.masterRepository = masterRepository;
        }

        public bool IsExistingClienteId(int clienteId)
        {
            var isExisting = masterRepository.Cliente.GetAll().Any(c =>
                c.ClienteId == clienteId);

            return isExisting;
        }
        public bool IsExistingCedula(string cedula)
        {
            var isExisting = masterRepository.Cliente.GetAll().Any(c =>
                c.Cedula == cedula);

            return isExisting;
        }
    }
}
