using System.ComponentModel.DataAnnotations;

namespace CapaModelo.Dtos
{
    public class ProveedorDTO
    {  
        public string Direccion { get; set; }
 
        public string Descripcion { get; set; }

        public string Telefono { get; set; }

        public string CorreoElectronico { get; set; }
    }
}
