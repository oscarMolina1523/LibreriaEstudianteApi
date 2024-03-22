using CapaDatos;
using CapaModelo;
using CapaModelo.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaEstudianteEndPoint
{

    [ApiController]
    [Route("Cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        [Route("ListarClientes")]
        public JsonResult Obtener()
        {
            List<Cliente> lista = CD_Cliente.Instancia.GetCliente();
            return new JsonResult(new { data = lista });
        }

        [HttpGet]
        [Route("Listar")]
        public string GetClientes()
        {
            var clientesJson = CD_Cliente.Instancia.GetCliente();
            return Newtonsoft.Json.JsonConvert.SerializeObject(clientesJson);
        }

        [HttpPost]
        [Route("Crear")]
        public JsonResult CrearCliente([FromBody] ClienteDTO clienteDto)
        {
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nombres = clienteDto.Nombres,
                Cedula= clienteDto.Cedula,
                Telefono= clienteDto.Telefono
            };

            var resultado = CD_Cliente.Instancia.RegistrarCliente(cliente);
            if (resultado)
            {
                return new JsonResult(new { message = "Cliente creada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al crear la cliente." });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public JsonResult ModificarCliente([FromBody] Cliente cliente)
        {
            var resultado = CD_Cliente.Instancia.ModificarCliente(cliente);
            if (resultado)
            {
                return new JsonResult(new { message = "Cliente modificada exitosamente." });
            }
            else
            {
                return new JsonResult(new { message = "Error al modificar la cliente." });
            }
        }
    }
}
