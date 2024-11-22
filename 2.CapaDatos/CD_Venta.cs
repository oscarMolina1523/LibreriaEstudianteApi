using CapaModelo;
using CapaModelo.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Venta
    {
        public static CD_Venta _instancia = null;

        private CD_Venta() { }

        public static CD_Venta Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Venta();
                }
                return _instancia;
            }
        }

        public bool RegistrarVenta(VentaDTO venta)
        {
            bool respuesta = true;
            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_AgregarVenta", objConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Guid idVenta = Guid.NewGuid(); // Generar un nuevo ID para la venta
                    cmd.Parameters.AddWithValue("@ID_VENTA", idVenta);
                    cmd.Parameters.AddWithValue("@NOMBRE_CLIENTE", venta.NombreCliente);
                    cmd.Parameters.AddWithValue("@NOMBRE_USUARIO", venta.NombreUsuario);
                    cmd.Parameters.AddWithValue("@FECHA", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TOTAL", venta.Total);

                    // Crear un DataTable para los detalles de la venta
                    DataTable dtDetalles = new DataTable();
                    dtDetalles.Columns.Add("NOMBRE_PRODUCTO", typeof(string)); // Cambiado a nombre del producto
                    dtDetalles.Columns.Add("CANTIDAD", typeof(int));
                    dtDetalles.Columns.Add("PRECIO_VENTA", typeof(decimal));

                    // Agregar los detalles al DataTable
                    foreach (var detalle in venta.Detalles)
                    {
                        dtDetalles.Rows.Add(detalle.NombreProducto, detalle.Cantidad, detalle.PrecioVenta);
                    }

                    // Agregar el parámetro de tipo tabla
                    SqlParameter paramDetalles = cmd.Parameters.AddWithValue("@DetallesVenta", dtDetalles);
                    paramDetalles.SqlDbType = SqlDbType.Structured;
                    paramDetalles.TypeName = "dbo.DetalleVentaType"; // Asegúrate de que el nombre coincida

                    objConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}