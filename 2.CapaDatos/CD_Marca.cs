using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Marca
    {
        public static CD_Marca _instancia = null;

        private CD_Marca()
        {

        }

        public static CD_Marca Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Marca();
                }

                return _instancia;
            }
        }

        public List<Marca> GetMarca()
        {
            var ListaMarca = new List<Marca>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_MarcasObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaMarca.Add(new Marca()
                        {
                            Id = (Guid)dr["ID_MARCA"],
                            Descripcion = dr["DESCRIPCION_MARCA"].ToString(),
                            Estado = Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaMarca;
                }
                catch
                {
                    ListaMarca = null;

                    return ListaMarca;
                }
            }
        }

        public bool RegistrarMarca(Marca marca)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarMarca", objConexion);

                    cmd.Parameters.AddWithValue("IdMarca", marca.Id);
                    cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion ?? "");
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

        public bool ModificarMarca(Marca marca)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarMarca", objConexion);

                    cmd.Parameters.AddWithValue("IdMarca", marca.Id);

                    cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Activo", marca.Estado);

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
