using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Medida")]
    public class MedidaController : ControllerBase
    {
        [HttpGet]
        [Route("ListarMedidas")]
        public JsonResult Obtener()
        {
            List<UnidadMedida> lista = CD_UnidadMedida.Instancia.GetMedida();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetMedidas()
        {
            var medidasJson = CD_UnidadMedida.Instancia.GetMedida();
            return Newtonsoft.Json.JsonConvert.SerializeObject(medidasJson);
        }

        [HttpPost]
        [Route("CrearMedida")]
        public JsonResult CrearMarca([FromBody] UnidadMedidaDTO unidadMedidaDto)
        {
            UnidadMedida unidadMedida = new UnidadMedida()
            {
                Id = Guid.NewGuid(),
                Descripcion = unidadMedidaDto.Descripcion,
                Estado = true,
            };

            var resultado = CD_UnidadMedida.Instancia.RegistrarMedida(unidadMedida);
            if (resultado)
            {
                return new JsonResult(new { message = "Medida creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la medida." });
            }
        }

        [HttpPut]
        [Route("ModificarMedida")]
        public JsonResult ModificarMedida([FromBody] UnidadMedida unidadMedida)
        {
            var resultado = CD_UnidadMedida.Instancia.ModificarMedida(unidadMedida);
            if (resultado)
            {
                return new JsonResult(new { message = "Medida modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la medida." });
            }
        }
    }
}
