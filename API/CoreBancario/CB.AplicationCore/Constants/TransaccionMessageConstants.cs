
namespace CB.AplicationCore.Constants
{
    public static class TransaccionMessageConstants
    {
        public const string NotExistingTransaccionId = "El Id de la transaccion no existe";
        public const string NotExistingTransaccionesinProductoOrigen = "El producto de origen no posee transacciones";
        public const string NotExistingProductoOrigenId = "El Id del producto origen no existe";
        public const string NotExistingProductoDestinoId = "El Id del producto destino no existe";
        public const string WrongMonto = "El monto no puede ser menor a 0";
        public const string EmptyTipoTransaccion = "El tipo de transccion es erroneo";
        public const string WrongTipoProductoOrigen = "El tipo de producto del producto origen esta erroneo";
        public const string WrongTipoProductoDestino = "El tipo de producto del producto destino esta erroneo";
        public const string MontoInsuficiente = "No posee balance suficiente para realizar esta transaccion";
    }
}
