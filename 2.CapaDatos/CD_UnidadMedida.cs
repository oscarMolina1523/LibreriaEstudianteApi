using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_UnidadMedida
    {
        public static CD_UnidadMedida _instancia = null;

        private CD_UnidadMedida()
        {

        }

        public static CD_UnidadMedida Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_UnidadMedida();
                }

                return _instancia;
            }
        }

        public List<UnidadMedida> GetMedida()
        {
            var ListaMedida = new List<UnidadMedida>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_MedidasObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaMedida.Add(new UnidadMedida()
                        {
                            Id = (Guid)dr["ID_UNIDAD_MEDIDA"],
                            Descripcion = dr["DESCRIPCION_MEDIDA"].ToString(),
                            Estado = Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaMedida;
                }
                catch
                {
                    ListaMedida = null;

                    return ListaMedida;
                }
            }
        }

        public bool RegistrarMedida(UnidadMedida unidadMedida)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarMedida", objConexion);

                    cmd.Parameters.AddWithValue("IdMedida", unidadMedida.Id);
                    cmd.Parameters.AddWithValue("Descripcion", unidadMedida.Descripcion ?? "");
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

        public bool ModificarMedida(UnidadMedida unidadMedida)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarMedida", objConexion);

                    cmd.Parameters.AddWithValue("IdMedida", unidadMedida.Id);

                    cmd.Parameters.AddWithValue("Descripcion", unidadMedida.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Activo", unidadMedida.Estado);

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
