using CB.Domain.Context;
using CB.Domain.Interfaces;

namespace CB.Domain.Repositories
{
    public class MasterRepository: IMasterRepository
    {
        readonly CoreBancoDbContext context;
        private IBeneficiarioRepository beneficiarioRepository;
        private IClienteRepository clienteRepository;
        private ICuentaAhorroRepository cuentaAhorroRepository;
        private IProductoRepository productoRepository;
        private ITarjetaCreditoRepository tarjetaCreditoRepository;
        private ITransaccionRepository transaccionRepository;
        private ITarjetaClaveRepository tarjetaClaveRepository;
        private IHistorialTransaccionRepository historialTransaccionRepository;

        public MasterRepository(CoreBancoDbContext context)
        {
            this.context = context;
        }

        public IBeneficiarioRepository Beneficiario
        {
            get
            {
                if (beneficiarioRepository == null)
                    beneficiarioRepository = new BeneficiarioRepository(context);

                return beneficiarioRepository;
            }
        }

        public IClienteRepository Cliente
        {
            get
            {
                if (clienteRepository == null)
                    clienteRepository = new ClienteRepository(context);

                return clienteRepository;
            }
        }

        public ICuentaAhorroRepository CuentaAhorro
        {
            get
            {
                if (cuentaAhorroRepository == null)
                    cuentaAhorroRepository = new CuentaAhorroRepository(context);

                return cuentaAhorroRepository;
            }
        }

        public IProductoRepository Producto
        {
            get
            {
                if (productoRepository == null)
                    productoRepository = new ProductoRepository(context);

                return productoRepository;
            }
        }

        public ITarjetaCreditoRepository TarjetaCredito
        {
            get
            {
                if (tarjetaCreditoRepository == null)
                    tarjetaCreditoRepository = new TarjetaCreditoRepository(context);

                return tarjetaCreditoRepository;
            }
        }

        public ITransaccionRepository Transaccion
        {
            get
            {
                if (transaccionRepository == null)
                    transaccionRepository = new TransaccionRepository(context);

                return transaccionRepository;
            }
        }
        public IHistorialTransaccionRepository HistorialTransaccion
        {
            get
            {
                if (historialTransaccionRepository == null)
                    historialTransaccionRepository = new HistorialTransaccionRepository(context);

                return historialTransaccionRepository;
            }
        }

        public ITarjetaClaveRepository TarjetaClave
        {
            get
            {
                if (tarjetaClaveRepository == null)
                    tarjetaClaveRepository = new TarjetaClaveRepository(context);

                return tarjetaClaveRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
