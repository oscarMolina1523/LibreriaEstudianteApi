using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Producto")]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("ListarProductos")]
        public JsonResult Obtener()
        {
            List<Producto> lista = CD_Producto.Instancia.GetProducto();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetProductos()
        {
            var productosJson = CD_Producto.Instancia.GetProducto();
            return Newtonsoft.Json.JsonConvert.SerializeObject(productosJson);
        }

        [HttpPost]
        [Route("CrearProducto")]
        public JsonResult CrearProducto([FromBody] ProductoDTO productoDto)
        {
            Producto producto = new Producto()
            {
                Id = Guid.NewGuid(),
                DescripcionProducto = productoDto.DescripcionProducto,
                IdCategoria= productoDto.IdCategoria,
                Estado = true,
            };

            var resultado = CD_Producto.Instancia.RegistrarProducto(producto);
            if (resultado)
            {
                return new JsonResult(new { message = "Producto creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la producto." });
            }
        }

        [HttpPut]
        [Route("ModificarProducto")]
        public JsonResult ModificarProducto([FromBody] Producto producto)
        {
            var resultado = CD_Producto.Instancia.ModificarProducto(producto);
            if (resultado)
            {
                return new JsonResult(new { message = "Producto modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la producto." });
            }
        }
    }
}
