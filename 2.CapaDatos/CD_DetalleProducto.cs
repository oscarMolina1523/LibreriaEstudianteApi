using CapaDatos;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;
using _1.CapaModelo;

namespace _2.CapaDatos
{
    public class CD_DetalleProducto
    {
        public static CD_DetalleProducto _instancia = null;

        private CD_DetalleProducto()
        {

        }

        public static CD_DetalleProducto Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleProducto();
                }

                return _instancia;
            }
        }

        public List<DetalleProducto> GetDetalleProducto()
        {
            var ListaDetalleProducto = new List<DetalleProducto>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_DetalleProductosObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaDetalleProducto.Add(new DetalleProducto()
                        {
                            Id = (Guid)dr["ID_DETALLE_PRODUCTO"],
                            IdUnidadMedida = (Guid)dr["ID_UNIDAD_MEDIDA"],
                            unidadMedida = new UnidadMedida() { Descripcion = dr["DESCRIPCION_MEDIDA"].ToString() },
                            IdMarca = (Guid)dr["ID_MARCA"],
                            marca = new Marca() { Descripcion = dr["DESCRIPCION_MARCA"].ToString() },
                            IdMaterial = (Guid)dr["ID_MATERIAL"],
                            material = new Material() { Descripcion = dr["DESCRIPCION_MATERIAL"].ToString() },
                            IdProducto = (Guid)dr["ID_PRODUCTO"],
                            producto = new Producto() { DescripcionProducto = dr["DESCRIPCION_PRODUCTO"].ToString() },
                        });
                    }
                    dr.Close();

                    return ListaDetalleProducto;
                }
                catch
                {
                    ListaDetalleProducto = null;

                    return ListaDetalleProducto;
                }
            }
        }

        public bool RegistrarDetalleProducto(DetalleProducto detalleProducto)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarDetalleProducto", objConexion);

                    cmd.Parameters.AddWithValue("IdDetalleProducto", detalleProducto.Id);
                    cmd.Parameters.AddWithValue("IdUnidadMedida", detalleProducto.IdUnidadMedida);
                    cmd.Parameters.AddWithValue("IdMarca", detalleProducto.IdMarca);
                    cmd.Parameters.AddWithValue("IdMaterial", detalleProducto.IdMaterial);
                    cmd.Parameters.AddWithValue("IdProducto", detalleProducto.IdProducto);
            
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

        public bool ModificarDetalleProducto(DetalleProducto detalleProducto)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarDetalleProducto", objConexion);

                    cmd.Parameters.AddWithValue("IdDetalleProducto", detalleProducto.Id);

                    cmd.Parameters.AddWithValue("IdUnidadMedida", detalleProducto.IdUnidadMedida);

                    cmd.Parameters.AddWithValue("IdMarca", detalleProducto.IdMarca);

                    cmd.Parameters.AddWithValue("IdMaterial", detalleProducto.IdMaterial);

                    cmd.Parameters.AddWithValue("IdProducto", detalleProducto.IdProducto);

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
