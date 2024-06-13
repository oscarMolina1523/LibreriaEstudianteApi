using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Material
    {
        public static CD_Material _instancia = null;

        private CD_Material()
        {

        }

        public static CD_Material Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Material();
                }

                return _instancia;
            }
        }

        public List<Material> GetMaterial()
        {
            var ListaMaterial = new List<Material>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_MaterialesObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaMaterial.Add(new Material()
                        {
                            Id = (Guid)dr["ID_MATERIAL"],
                            Descripcion = dr["DESCRIPCION_MATERIAL"].ToString(),
                            Estado = dr["ESTADO"] == DBNull.Value ? false : Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaMaterial;
                }
                catch
                {
                    ListaMaterial = null;

                    return ListaMaterial;
                }
            }
        }

        public bool RegistrarMaterial(Material material)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarMaterial", objConexion);

                    cmd.Parameters.AddWithValue("IdMaterial", material.Id);
                    cmd.Parameters.AddWithValue("Descripcion", material.Descripcion ?? "");
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

        public bool ModificarMaterial(Material material)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarMaterial", objConexion);

                    cmd.Parameters.AddWithValue("IdMaterial", material.Id);

                    cmd.Parameters.AddWithValue("Descripcion", material.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Activo", material.Estado);

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
