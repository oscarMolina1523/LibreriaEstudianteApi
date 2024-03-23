using System.ComponentModel.DataAnnotations;

namespace _1.CapaModelo.Dtos
{
    public class UsuarioDTO
    {
        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

        public Guid IdEmpleado { get; set; }

        public Guid IdRol { get; set; }
    }
}
