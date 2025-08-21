using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Datos
{
    public class RutaRepository
    {
        private readonly ConexionBD conexion;

        public RutaRepository()
        {
            conexion = ConexionBD.Instance;
        }

        public bool Crear(Ruta ruta)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spCrearRuta", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", ruta.Nombre);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<Ruta> ObtenerTodos()
        {
            var rutas = new List<Ruta>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerRutas", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rutas.Add(new Ruta
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Disponible = Convert.ToBoolean(reader["Disponible"])
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return rutas;
        }

        public List<Ruta> ObtenerDisponibles()
        {
            var rutas = new List<Ruta>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerRutasDisponibles", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rutas.Add(new Ruta
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return rutas;
        }

        public bool Actualizar(Ruta ruta)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spActualizarRuta", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", ruta.Id);
                    cmd.Parameters.AddWithValue("@Nombre", ruta.Nombre);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spEliminarRuta", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public Ruta ObtenerPorId(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerRutaPorId", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Ruta
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Disponible = Convert.ToBoolean(reader["Disponible"])
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

        public Ruta ObtenerPorNombre(string nombre)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerRutaPorNombre", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Ruta
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Disponible = Convert.ToBoolean(reader["Disponible"])
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

        public bool ExisteRuta(string nombre)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spExisteRuta", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public bool EstaDisponible(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spRutaDisponible", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RutaId", id);

                    return (bool)cmd.ExecuteScalar();
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public bool TieneAsignacionesActivas(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spRutaTieneAsignacionesActivas", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RutaId", id);

                    return (bool)cmd.ExecuteScalar();
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public void MarcarComoDisponible(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spMarcarRutaDisponible", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public void MarcarComoNoDisponible(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spMarcarRutaNoDisponible", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}
