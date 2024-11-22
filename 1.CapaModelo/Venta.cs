namespace CapaModelo
{
    public class Venta
    {
        public Guid Id { get; set; }
        public string NombreCliente { get; set; } 
        public string NombreUsuario { get; set; } 
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }

    public class DetalleVenta
    {
        public string NombreProducto { get; set; } 
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}