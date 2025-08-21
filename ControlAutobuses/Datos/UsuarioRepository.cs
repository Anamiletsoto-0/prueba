using System;
using System.Data;
using System.Data.SqlClient;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Datos
{
    public class UsuarioRepository
    {
        private readonly ConexionBD conexion;

        public UsuarioRepository()
        {
            conexion = ConexionBD.Instance;
        }

        public Usuario Autenticar(string nombreUsuario, string contrasena)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spAutenticarUsuario", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasena);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                TipoUsuario = reader["TipoUsuario"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}