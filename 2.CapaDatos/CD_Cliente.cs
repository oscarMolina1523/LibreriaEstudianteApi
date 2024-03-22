using CapaModelo;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Cliente
    {
        public static CD_Cliente _instancia = null;

        private CD_Cliente()
        {

        }

        public static CD_Cliente Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Cliente();
                }

                return _instancia;
            }
        }

        public List<Cliente> GetCliente()
        {
            var ListaCliente = new List<Cliente>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ClientesObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaCliente.Add(new Cliente()
                        {
                            Id = (Guid)dr["ID_CLIENTE"],
                            Nombres = dr["NOMBRES"].ToString(),
                            Cedula = dr["CEDULA"].ToString(),
                            Telefono= dr["TELEFONO"].ToString()

                        });
                    }
                    dr.Close();

                    return ListaCliente;
                }
                catch
                {
                    ListaCliente = null;

                    return ListaCliente;
                }

            }
        }

        public bool RegistrarCliente(Cliente cliente)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarCliente", objConexion);

                    cmd.Parameters.AddWithValue("IdCliente", cliente.Id);
                    cmd.Parameters.AddWithValue("Nombres", cliente.Nombres ?? "");
                    cmd.Parameters.AddWithValue("Cedula", cliente.Cedula ?? "");
                    cmd.Parameters.AddWithValue("Telefono", cliente.Telefono ?? "");
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

        public bool ModificarCliente(Cliente cliente)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarCliente", objConexion);

                    cmd.Parameters.AddWithValue("IdCliente", cliente.Id);

                    cmd.Parameters.AddWithValue("Nombres", cliente.Nombres ?? "");

                    cmd.Parameters.AddWithValue("Cedula", cliente.Cedula ?? "");

                    cmd.Parameters.AddWithValue("Telefono", cliente.Telefono ?? "");

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
