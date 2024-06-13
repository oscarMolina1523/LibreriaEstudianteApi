using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Producto
    {
        public static CD_Producto _instancia = null;

        private CD_Producto()
        {

        }

        public static CD_Producto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Producto();
                }

                return _instancia;
            }
        }

        public List<Producto> GetProducto()
        {
            var ListaProducto = new List<Producto>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ProductosObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaProducto.Add(new Producto()
                        {
                            Id = (Guid)dr["ID_PRODUCTO"],
                            DescripcionProducto = dr["DESCRIPCION_PRODUCTO"].ToString(),
                            IdCategoria = (Guid)dr["ID_CATEGORIA"],
                            categoria = new Categoria() { Descripcion = dr["DESCRIPCION_CATEGORIA"].ToString() },
                            Estado = dr["ESTADO"] == DBNull.Value ? false : Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaProducto;
                }
                catch
                {
                    ListaProducto = null;

                    return ListaProducto;
                }
            }
        }

        public bool RegistrarProducto(Producto producto)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarProducto", objConexion);

                    cmd.Parameters.AddWithValue("IdProducto", producto.Id);
                    cmd.Parameters.AddWithValue("Descripcion", producto.DescripcionProducto ?? "");
                    cmd.Parameters.AddWithValue("IdCategoria", producto.IdCategoria);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch
                {
                    respuesta = false;
                }

                return respuesta;
            }
        }

        public bool ModificarProducto(Producto producto)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarProducto", objConexion);

                    cmd.Parameters.AddWithValue("IdProducto", producto.Id);

                    cmd.Parameters.AddWithValue("Descripcion", producto.DescripcionProducto ?? "");

                    cmd.Parameters.AddWithValue("IdCategoria", producto.IdCategoria);

                    cmd.Parameters.AddWithValue("Activo", producto.Estado);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    objConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch
                {
                    respuesta = false;
                }

                return respuesta;
            }
        }
    }
}
