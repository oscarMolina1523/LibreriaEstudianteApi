using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{
    [ApiController]
    [Route("Proveedor")]
    public class ProveedorController : ControllerBase
    {
        [HttpGet]
        [Route("ListarProveedores")]
        public JsonResult Obtener()
        {
            List<Proveedor> lista = CD_Proveedor.Instancia.GetProveedor();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetProveedores()
        {
            var proveedoresJson = CD_Proveedor.Instancia.GetProveedor();
            return Newtonsoft.Json.JsonConvert.SerializeObject(proveedoresJson);
        }

        [HttpPost]
        [Route("Crear")]
        public JsonResult CrearProveedor([FromBody] ProveedorDTO proveedorDto)
        {
            Proveedor proveedor = new Proveedor()
            {
                Id = Guid.NewGuid(),
                Descripcion = proveedorDto.Descripcion,
                Direccion = proveedorDto.Direccion,
                Telefono = proveedorDto.Telefono,
                CorreoElectronico= proveedorDto.CorreoElectronico
            };

            var resultado = CD_Proveedor.Instancia.RegistrarProveedor(proveedor);
            if (resultado)
            {
                return new JsonResult(new { message = "Proveedor creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la proveedor." });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public JsonResult ModificarProveedor([FromBody] Proveedor proveedor)
        {
            var resultado = CD_Proveedor.Instancia.ModificarProveedor(proveedor);
            if (resultado)
            {
                return new JsonResult(new { message = "Proveedor modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la proveedor." });
            }
        }
    }
}
