using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Material")]
    public class MaterialController : ControllerBase
    {
        [HttpGet]
        [Route("ListarMateriales")]
        public JsonResult Obtener()
        {
            List<Material> lista = CD_Material.Instancia.GetMaterial();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetMateriales()
        {
            var materialesJson = CD_Material.Instancia.GetMaterial();
            return Newtonsoft.Json.JsonConvert.SerializeObject(materialesJson);
        }

        [HttpPost]
        [Route("CrearMaterial")]
        public JsonResult CrearMaterial([FromBody] MaterialDTO materialDto)
        {
            Material material = new Material()
            {
                Id = Guid.NewGuid(),
                Descripcion = materialDto.Descripcion,
                Estado = true,
            };

            var resultado = CD_Material.Instancia.RegistrarMaterial(material);
            if (resultado)
            {
                return new JsonResult(new { message = "Material creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la material." });
            }
        }

        [HttpPut]
        [Route("ModificarMaterial")]
        public JsonResult ModificarMaterial([FromBody] Material material)
        {
            var resultado = CD_Material.Instancia.ModificarMaterial(material);
            if (resultado)
            {
                return new JsonResult(new { message = "Material modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la material." });
            }
        }
    }
}
