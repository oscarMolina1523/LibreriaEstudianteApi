using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Rol
    {
        public static CD_Rol _instancia = null;

        private CD_Rol()
        {

        }

        public static CD_Rol Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Rol();
                }

                return _instancia;
            }
        }

        public List<Rol> GetRol()
        {
            var ListaRol = new List<Rol>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_RolesObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaRol.Add(new Rol()
                        {
                            Id = (Guid)dr["ID_ROLES"],
                            Descripcion = dr["DESCRIPCION_ROLES"].ToString(),
                            Estado = dr["ESTADO"] == DBNull.Value ? false : Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaRol;
                }
                catch
                {
                    ListaRol = null;

                    return ListaRol;
                }
            }
        }

        public bool RegistrarRol(Rol rol)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarRol", objConexion);

                    cmd.Parameters.AddWithValue("IdRol", rol.Id);
                    cmd.Parameters.AddWithValue("Descripcion", rol.Descripcion ?? "");
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

        public bool ModificarRol(Rol rol)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarRol", objConexion);

                    cmd.Parameters.AddWithValue("IdRol", rol.Id);

                    cmd.Parameters.AddWithValue("Descripcion", rol.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Activo", rol.Estado);

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
