using CapaDatos;
using CapaModelo;
using System.Data.SqlClient;
using System.Data;
using _1.CapaModelo;

namespace _2.CapaDatos
{
    public class CD_Usuario
    {
        public static CD_Usuario _instancia = null;

        private CD_Usuario()
        {

        }

        public static CD_Usuario Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Usuario();
                }

                return _instancia;
            }
        }

        public List<Usuario> GetUsuario()
        {
            var ListaUsuario = new List<Usuario>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_UsuariosObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaUsuario.Add(new Usuario()
                        {
                            Id = (Guid)dr["ID_USUARIO"],
                            IdRol = (Guid)dr["ID_ROLES"],
                            rol = new Rol() { Descripcion = dr["DESCRIPCION_ROLES"].ToString() },
                            IdEmpleado = (Guid)dr["ID_EMPLEADO"],
                            empleado = new Empleado() { Nombres = dr["NOMBRES"].ToString() , Apellidos = dr["APELLIDOS"].ToString() },
                            NombreUsuario = dr["NOMBRE_USUARIO"].ToString(),
                            Contraseña = dr["CONTRASEÑA"].ToString(),
                            Estado = Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaUsuario;
                }
                catch
                {
                    ListaUsuario = null;

                    return ListaUsuario;
                }
            }
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarUsuario", objConexion);

                    cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);
                    cmd.Parameters.AddWithValue("IdRol", usuario.IdRol);
                    cmd.Parameters.AddWithValue("IdEmpleado", usuario.IdEmpleado);
                    cmd.Parameters.AddWithValue("NombreUsuario", usuario.NombreUsuario ?? "");
                    cmd.Parameters.AddWithValue("Contraseña", usuario.Contraseña ?? "");
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

        public bool ModificarUsuario(Usuario usuario)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarUsuario", objConexion);

                    cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                    cmd.Parameters.AddWithValue("IdRol", usuario.IdRol);

                    cmd.Parameters.AddWithValue("IdEmpleado", usuario.IdEmpleado);

                    cmd.Parameters.AddWithValue("NombreUsuario", usuario.NombreUsuario ?? "");

                    cmd.Parameters.AddWithValue("Contraseña", usuario.Contraseña ?? "");

                    cmd.Parameters.AddWithValue("Estado", usuario.Estado);

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
