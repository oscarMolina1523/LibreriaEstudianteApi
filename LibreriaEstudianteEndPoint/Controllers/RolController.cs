using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Rol")]
    public class RolController : ControllerBase
    {
        [HttpGet]
        [Route("ListarRoles")]
        public JsonResult Obtener()
        {
            List<Rol> lista = CD_Rol.Instancia.GetRol();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetRoles()
        {
            var rolesJson = CD_Rol.Instancia.GetRol();
            return Newtonsoft.Json.JsonConvert.SerializeObject(rolesJson);
        }

        [HttpPost]
        [Route("CrearRol")]
        public JsonResult CrearRol([FromBody] RolDTO rolDto)
        {
            Rol rol = new Rol()
            {
                Id = Guid.NewGuid(),
                Descripcion = rolDto.Descripcion,
                Estado = true,
            };

            var resultado = CD_Rol.Instancia.RegistrarRol(rol);
            if (resultado)
            {
                return new JsonResult(new { message = "Rol creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la rol." });
            }
        }

        [HttpPut]
        [Route("ModificarRol")]
        public JsonResult ModificarRol([FromBody] Rol rol)
        {
            var resultado = CD_Rol.Instancia.ModificarRol(rol);
            if (resultado)
            {
                return new JsonResult(new { message = "Rol modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la rol." });
            }
        }
    }
}
