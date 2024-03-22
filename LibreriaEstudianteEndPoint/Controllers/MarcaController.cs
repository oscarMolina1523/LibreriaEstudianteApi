using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Marca")]
    public class MarcaController : ControllerBase
    {
        [HttpGet]
        [Route("ListarMarcas")]
        public JsonResult Obtener()
        {
            List<Marca> lista = CD_Marca.Instancia.GetMarca();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetMarcas()
        {
            var marcasJson = CD_Marca.Instancia.GetMarca();
            return Newtonsoft.Json.JsonConvert.SerializeObject(marcasJson);
        }

        [HttpPost]
        [Route("CrearMarca")]
        public JsonResult CrearMarca([FromBody] MarcaDTO marcaDto)
        {
            Marca marca = new Marca()
            {
                Id = Guid.NewGuid(),
                Descripcion = marcaDto.Descripcion,
                Estado = true,
            };

            var resultado = CD_Marca.Instancia.RegistrarMarca(marca);
            if (resultado)
            {
                return new JsonResult(new { message = "Marca creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la marca." });
            }
        }

        [HttpPut]
        [Route("ModificarMarca")]
        public JsonResult ModificarMarca([FromBody] Marca marca)
        {
            var resultado = CD_Marca.Instancia.ModificarMarca(marca);
            if (resultado)
            {
                return new JsonResult(new { message = "Marca modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la marca." });
            }
        }
    }
}
