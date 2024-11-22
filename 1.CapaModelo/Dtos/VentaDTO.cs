namespace CapaModelo.Dtos
{
    public class VentaDTO
    {
        public string NombreCliente { get; set; } 
        public string NombreUsuario { get; set; } 
        public decimal Total { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; }
    }

    public class DetalleVentaDTO
    {
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}