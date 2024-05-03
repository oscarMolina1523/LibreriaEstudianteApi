using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Categoria")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("ListarCategorias")]
        public JsonResult Obtener()
        {
            List<Categoria> lista = CD_Categoria.Instancia.GetCategoria();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetCategorias()
        {
            var categoriasJson = CD_Categoria.Instancia.GetCategoria();
            return Newtonsoft.Json.JsonConvert.SerializeObject(categoriasJson);
        }

        [HttpPost]
        [Route("CrearCategoria")]
        public JsonResult CrearCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            Categoria categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Descripcion = categoriaDto.Descripcion,
                Estado=true,
            };

            var resultado = CD_Categoria.Instancia.RegistrarCategoria(categoria);
            if (resultado)
            {
                return new JsonResult(new { message = "Categoría creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la categoría." });
            }
        }

        [HttpPut]
        [Route("ModificarCategoria")]
        public JsonResult ModificarCategoria([FromBody] Categoria categoria)
        {
            var resultado = CD_Categoria.Instancia.ModificarCategoria(categoria);
            if (resultado)
            {
                return new JsonResult(new { message = "Categoría modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la categoría." });
            }
        }

        [HttpDelete]
        [Route("EliminarCategoria/{id}")]
        public JsonResult EliminarCategoria(Guid id)
        {
            var resultado = CD_Categoria.Instancia.EliminarCategoria(id);
            if (resultado)
            {
                return new JsonResult(new { message = "Categoría eliminada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al eliminar la categoría." });
            }
        }



    }
}
