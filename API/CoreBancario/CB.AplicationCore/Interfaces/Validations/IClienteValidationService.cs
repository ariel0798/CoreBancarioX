

namespace CB.AplicationCore.Interfaces.Validations
{
    public interface IClienteValidationService 
    {
        bool IsExistingClienteId(int clienteId);
        bool IsExistingCedula(string cedula);
    }
}
