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
    [Route("DetalleProducto")]
    public class DetalleProductoController : ControllerBase
    {
        [HttpGet]
        [Route("ListarDetalleProductos")]
        public JsonResult Obtener()
        {
            List<DetalleProducto> lista = CD_DetalleProducto.Instancia.GetDetalleProducto();
            return new JsonResult(new { data = lista });
        }

        [HttpPost]
        [Route("CrearDetalleProducto")]
        public JsonResult CrearDetalleProducto([FromBody] DetalleProductoDTO detalleProductoDto)
        {
            DetalleProducto detalleProducto = new DetalleProducto()
            {
                Id = Guid.NewGuid(),
                IdUnidadMedida = detalleProductoDto.IdUnidadMedida,
                IdMarca = detalleProductoDto.IdMarca,
                IdMaterial= detalleProductoDto.IdMaterial,
                IdProducto= detalleProductoDto.IdProducto
            };

            var resultado = CD_DetalleProducto.Instancia.RegistrarDetalleProducto(detalleProducto);
            if (resultado)
            {
                return new JsonResult(new { message = "Detalle Producto creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Detalle Error al crear la producto." });
            }
        }

        [HttpPut]
        [Route("ModificarDetalleProducto")]
        public JsonResult ModificarDetalleProducto([FromBody] DetalleProducto detalleProducto)
        {
            var resultado = CD_DetalleProducto.Instancia.ModificarDetalleProducto(detalleProducto);
            if (resultado)
            {
                return new JsonResult(new { message = "Detalle Producto modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la detalle producto." });
            }
        }
    }
}
