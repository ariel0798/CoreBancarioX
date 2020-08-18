

namespace CB.Domain.Interfaces
{
    public interface IMasterRepository
    {
        IBeneficiarioRepository Beneficiario { get; }
        IClienteRepository Cliente { get; }
        ICuentaAhorroRepository CuentaAhorro { get; }
        IProductoRepository Producto { get; }
        ITarjetaCreditoRepository TarjetaCredito { get; }
        ITransaccionRepository Transaccion { get; }
        void Save();
    }
}
