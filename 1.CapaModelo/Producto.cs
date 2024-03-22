namespace CapaModelo
{
    public class Producto : BaseEntity
    {
        public string DescripcionProducto { get; set; }

        public Guid IdCategoria { get; set; }

        public bool Estado { get; set; }
    }
}
