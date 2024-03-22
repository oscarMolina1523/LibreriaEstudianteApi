using System.ComponentModel.DataAnnotations;

namespace CapaModelo.Dtos
{
    public class EmpleadoDTO
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Cedula { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }
    }
}
