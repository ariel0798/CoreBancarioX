
namespace CB.AplicationCore.Constants
{
    public static class TransaccionMessageConstants
    {
        public const string NotExistingTransaccionId = "El Id de la transaccion no existe";
        public const string NotExistingRowUid = "El RowUid de la transaccion no existe";
        public const string NotExistingTransaccionesinProductoOrigen = "El producto de origen no posee transacciones";
        public const string NotExistingProductoOrigenId = "El Id del producto origen no existe";
        public const string NotExistingProductoDestinoId = "El Id del producto destino no existe";
        public const string WrongMonto = "El monto no puede ser 0 o menor a 0";
        public const string EmptyTipoTransaccion = "El tipo de transccion es erroneo";
        public const string WrongTipoProductoOrigen = "El tipo de producto del producto origen esta erroneo";
        public const string WrongTipoProductoDestino = "El tipo de producto del producto destino esta erroneo";
        public const string MontoInsuficiente = "No posee balance suficiente para realizar esta transaccion";
        public const string WrongClave = "La clave es incorrecta";
        public const string NotE = "La clave es incorrecta";


        public const string TransaccionRechazada = "Transaccion rechazada, el motivo es: ";
        public const string TransaccionProcesada = "Esta transaccion se encuentra procesada";
        public const string EstadoTransaccionErroneo = "Error en estado de transaccion";

        public const string TiempoClaveAgotado = "Se agoto el tiempo para introducir la clave, se debe realizar otra transaccion";
        public const string PagoSobrepasaLimiteCredito = "El pago sobrepasa el limite de credito";

        public const string CargoInteresPorNoPago = "Cargo de interes por no pago del corte";



    }
}
