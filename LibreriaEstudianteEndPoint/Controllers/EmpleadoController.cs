using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Empleado")]
    public class EmpleadoController : ControllerBase
    {
        [HttpGet]
        [Route("ListarEmpleados")]
        public JsonResult Obtener()
        {
            List<Empleado> lista = CD_Empleado.Instancia.GetEmpleado();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetEmpleados()
        {
            var empleadosJson = CD_Empleado.Instancia.GetEmpleado();
            return Newtonsoft.Json.JsonConvert.SerializeObject(empleadosJson);
        }

        [HttpPost]
        [Route("Crear")]
        public JsonResult CrearEmpleado([FromBody] EmpleadoDTO empleadoDto)
        {
            Empleado empleado = new Empleado()
            {
                Id = Guid.NewGuid(),
                Nombres = empleadoDto.Nombres,
                Apellidos= empleadoDto.Apellidos,
                Cedula = empleadoDto.Cedula,
                Telefono = empleadoDto.Telefono,
                Estado= true,
            };

            var resultado = CD_Empleado.Instancia.RegistrarEmpleado(empleado);
            if (resultado)
            {
                return new JsonResult(new { message = "Empleado creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la Empleado." });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public JsonResult ModificarEmpleadp([FromBody] Empleado empleado)
        {
            var resultado = CD_Empleado.Instancia.ModificarEmpleado(empleado);
            if (resultado)
            {
                return new JsonResult(new { message = "Empleado modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la empleado." });
            }
        }
    }
}
