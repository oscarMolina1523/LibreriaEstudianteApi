using CapaModelo;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Empleado
    {
        public static CD_Empleado _instancia = null;

        private CD_Empleado()
        {

        }

        public static CD_Empleado Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Empleado();
                }

                return _instancia;
            }
        }

        public List<Empleado> GetEmpleado()
        {
            var ListaEmpleado = new List<Empleado>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_EmpleadosObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaEmpleado.Add(new Empleado()
                        {
                            Id = (Guid)dr["ID_EMPLEADO"],
                            Nombres = dr["NOMBRES"].ToString(),
                            Apellidos = dr["APELLIDOS"].ToString(),
                            Cedula = dr["CEDULA"].ToString(),
                            Telefono = dr["TELEFONO"].ToString(),
                            Estado = dr["ESTADO"] == DBNull.Value ? false : Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaEmpleado;
                }
                catch
                {
                    ListaEmpleado = null;

                    return ListaEmpleado;
                }
            }
        }

        public bool RegistrarEmpleado(Empleado empleado)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarEmpleado", objConexion);

                    cmd.Parameters.AddWithValue("IdEmpleado", empleado.Id);
                    cmd.Parameters.AddWithValue("Nombres", empleado.Nombres ?? "");
                    cmd.Parameters.AddWithValue("Apellidos", empleado.Apellidos ?? "");
                    cmd.Parameters.AddWithValue("Cedula", empleado.Cedula ?? "");
                    cmd.Parameters.AddWithValue("Telefono", empleado.Telefono ?? "");
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

        public bool ModificarEmpleado(Empleado empleado)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarEmpleado", objConexion);

                    cmd.Parameters.AddWithValue("IdEmpleado", empleado.Id);

                    cmd.Parameters.AddWithValue("Nombres", empleado.Nombres ?? "");

                    cmd.Parameters.AddWithValue("Apellidos", empleado.Apellidos ?? "");

                    cmd.Parameters.AddWithValue("Cedula", empleado.Cedula ?? "");

                    cmd.Parameters.AddWithValue("Telefono", empleado.Telefono ?? "");

                    cmd.Parameters.AddWithValue("Estado", empleado.Estado);

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
