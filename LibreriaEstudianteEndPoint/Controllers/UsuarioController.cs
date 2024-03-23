using _1.CapaModelo;
using _1.CapaModelo.Dtos;
using _2.CapaDatos;
using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("ListarUsuarios")]
        public JsonResult Obtener()
        {
            List<Usuario> lista = CD_Usuario.Instancia.GetUsuario();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetUsuarios()
        {
            var usuariosJson = CD_Usuario.Instancia.GetUsuario();
            return Newtonsoft.Json.JsonConvert.SerializeObject(usuariosJson);
        }

        [HttpPost]
        [Route("Crear")]
        public JsonResult CrearUsuario([FromBody] UsuarioDTO usuarioDto)
        {
            Usuario usuario = new Usuario()
            {
                Id = Guid.NewGuid(),
                IdRol= usuarioDto.IdRol,
                IdEmpleado= usuarioDto.IdEmpleado,
                NombreUsuario = usuarioDto.NombreUsuario,
                Contraseña = usuarioDto.Contraseña,
                Estado= true,
            };

            var resultado = CD_Usuario.Instancia.RegistrarUsuario(usuario);
            if (resultado)
            {
                return new JsonResult(new { message = "Usuario creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la usuario." });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public JsonResult ModificarUsuario([FromBody] Usuario usuario)
        {
            var resultado = CD_Usuario.Instancia.ModificarUsuario(usuario);
            if (resultado)
            {
                return new JsonResult(new { message = "Usuario modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la usuario." });
            }
        }
    }
}
