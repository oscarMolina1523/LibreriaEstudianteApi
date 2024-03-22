using CapaModelo;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Categoria
    {
        public static CD_Categoria _instancia = null;

        private CD_Categoria()
        {

        }

        public static CD_Categoria Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Categoria();
                }

                return _instancia;
            }
        }

        public List<Categoria> GetCategoria()
        {
            var ListaCategoria = new List<Categoria>();

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                SqlCommand cmd = new SqlCommand("USP_CategoriasObtener", objConexion);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ListaCategoria.Add(new Categoria()
                        {
                            Id = (Guid)dr["ID_CATEGORIA"],
                            Descripcion = dr["DESCRIPCION_CATEGORIA"].ToString(),
                            Estado = Convert.ToBoolean(dr["ESTADO"].ToString())

                        });
                    }
                    dr.Close();

                    return ListaCategoria;
                }
                catch
                {
                    ListaCategoria = null;

                    return ListaCategoria;
                }
            }
        }

        public bool RegistrarCategoria(Categoria categoria)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_RegistrarCategoria", objConexion);

                    cmd.Parameters.AddWithValue("IdCategoria", categoria.Id);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion ?? "");
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


        public bool ModificarCategoria(Categoria categoria)
        {
            bool respuesta = true;

            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ModificarCategoria", objConexion);

                    cmd.Parameters.AddWithValue("IdCategoria", categoria.Id);

                    //cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion = (categoria.Descripcion !=
                    //null ? categoria.Descripcion : ""));

                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion ?? "");

                    cmd.Parameters.AddWithValue("Activo", categoria.Estado);

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

        public bool EliminarCategoria(Guid IdCategoria)
        {
            bool respuesta = true;
            using (SqlConnection objConexion = new SqlConnection(Conexion.Con()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_CategoriaEliminar", objConexion);
                    cmd.Parameters.AddWithValue("cod", IdCategoria);
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
            }
            return respuesta;
        }
    }
}
