using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Venta")]
    public class VentaController : ControllerBase
    {
        // [HttpGet]
        // [Route("ListarVentas")]
        // public JsonResult ListarVentas()
        // {
            
        //     List<Venta> lista = CD_Venta.Instancia.ListarVentas(); 
        //     return new JsonResult(new { data = lista });
        // }

        [HttpPost]
        [Route("CrearVenta")]
        public JsonResult CrearVenta([FromBody] VentaDTO ventaDto)
        {
            // Crear una nueva instancia de Venta
            var nuevaVenta = new VentaDTO
            {
                NombreCliente = ventaDto.NombreCliente,
                NombreUsuario = ventaDto.NombreUsuario,
                Total = ventaDto.Total,
                Detalles = ventaDto.Detalles
            };

            var resultado = CD_Venta.Instancia.RegistrarVenta(nuevaVenta);
            if (resultado)
            {
                return new JsonResult(new { message = "Venta creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la venta." });
            }
        }

        // [HttpPut]
        // [Route("ModificarVenta")]
        // public JsonResult ModificarVenta([FromBody] Venta venta)
        // {
            
        //     var resultado = CD_Venta.Instancia.ModificarVenta(venta); 
        //     if (resultado)
        //     {
        //         return new JsonResult(new { message = "Venta modificada exitosamente." });
        //     }
        //     else
        //     {
        //         return new JsonResult(new { message = "Error al modificar la venta." });
        //     }
        // }

        // [HttpDelete]
        // [Route("EliminarVenta/{id}")]
        // public JsonResult EliminarVenta(Guid id)
        // {
        //     var resultado = CD_Venta.Instancia.EliminarVenta(id); 
        //     if (resultado)
        //     {
        //         return new JsonResult(new { message = "Venta eliminada exitosamente." });
        //     }
        //     else
        //     {
        //         return new JsonResult(new { message = "Error al eliminar la venta." });
        //     }
        // }
    }
}