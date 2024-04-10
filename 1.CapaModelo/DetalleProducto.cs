using CapaModelo;

namespace _1.CapaModelo
{
    public class DetalleProducto : BaseEntity
    {
        public Guid IdUnidadMedida { get; set; }

        public UnidadMedida unidadMedida { get; set; }

        public Guid IdMarca { get; set; }

        public Marca marca { get; set; }

        public Guid IdMaterial { get; set; }

        public Material material { get; set; }

        public Guid IdProducto { get; set; }

        public Producto producto { get; set; }
    }
}
