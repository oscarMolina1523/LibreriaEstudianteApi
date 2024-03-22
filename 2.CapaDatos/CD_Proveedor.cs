using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Proveedor
    {
        public static CD_Proveedor _instancia = null;

        private CD_Proveedor()
        {

        }

        public static CD_Proveedor Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Proveedor();
                }

                return _instancia;
            }
        }

        public List<Proveedor> GetProveedor()
        {
            var ListaProveedor = new List<Proveedor>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_ProveedoresObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaProveedor.Add(new Proveedor()
                        {
                            Id = (Guid)dr["ID_PROVEEDOR"],
                            Direccion = dr["DIRECCION"].ToString(),
                            CorreoElectronico = dr["CORREO_ELECTRONICO"].ToString(),
                            Telefono = dr["TELEFONO"].ToString(),
                            Descripcion = dr["DESCRIPCION_PROVEEDOR"].ToString(),

                        });
                    }
                    dr.Close();

                    return ListaProveedor;
                }
                catch
                {
                    ListaProveedor = null;

                    return ListaProveedor;
                }

            }
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarProveedor", objConexion);

                    cmd.Parameters.AddWithValue("IdProveedor", proveedor.Id);
                    cmd.Parameters.AddWithValue("Descripcion", proveedor.Descripcion ?? "");
                    cmd.Parameters.AddWithValue("Direccion", proveedor.Direccion ?? "");
                    cmd.Parameters.AddWithValue("Telefono", proveedor.Telefono ?? "");
                    cmd.Parameters.AddWithValue("CorreoElectronico", proveedor.CorreoElectronico ?? "");
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

        public bool ModificarProveedor(Proveedor proveedor)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarProveedor", objConexion);

                    cmd.Parameters.AddWithValue("IdProveedor", proveedor.Id);

                    cmd.Parameters.AddWithValue("Descripcion", proveedor.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Direccion", proveedor.Direccion ?? "");

                    cmd.Parameters.AddWithValue("Telefono", proveedor.Telefono ?? "");

                    cmd.Parameters.AddWithValue("CorreoElectronico", proveedor.CorreoElectronico ?? "");

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
