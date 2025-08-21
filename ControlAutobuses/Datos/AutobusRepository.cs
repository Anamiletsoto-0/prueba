using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Datos
{
    public class AutobusRepository
    {
        private readonly ConexionBD conexion;

        public AutobusRepository()
        {
            conexion = ConexionBD.Instance;
        }

        public bool Crear(Autobus autobus)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spCrearAutobus", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Marca", autobus.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", autobus.Modelo);
                    cmd.Parameters.AddWithValue("@Placa", autobus.Placa);
                    cmd.Parameters.AddWithValue("@Color", autobus.Color);
                    cmd.Parameters.AddWithValue("@Anio", autobus.Anio);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<Autobus> ObtenerTodos()
        {
            var autobuses = new List<Autobus>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAutobuses", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autobuses.Add(new Autobus
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                Color = reader["Color"].ToString(),
                                Anio = Convert.ToInt32(reader["Anio"]),
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

            return autobuses;
        }

        public List<Autobus> ObtenerDisponibles()
        {
            var autobuses = new List<Autobus>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAutobusesDisponibles", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autobuses.Add(new Autobus
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                Color = reader["Color"].ToString(),
                                Anio = Convert.ToInt32(reader["Anio"])
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return autobuses;
        }

        public bool Actualizar(Autobus autobus)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spActualizarAutobus", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", autobus.Id);
                    cmd.Parameters.AddWithValue("@Marca", autobus.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", autobus.Modelo);
                    cmd.Parameters.AddWithValue("@Placa", autobus.Placa);
                    cmd.Parameters.AddWithValue("@Color", autobus.Color);
                    cmd.Parameters.AddWithValue("@Anio", autobus.Anio);

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
                using (SqlCommand cmd = new SqlCommand("spEliminarAutobus", conexion.GetConnection()))
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

        public Autobus ObtenerPorId(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAutobusPorId", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Autobus
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                Color = reader["Color"].ToString(),
                                Anio = Convert.ToInt32(reader["Anio"]),
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

        public bool EstaDisponible(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spAutobusDisponible", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AutobusId", id);

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
                using (SqlCommand cmd = new SqlCommand("spAutobusTieneAsignacionesActivas", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AutobusId", id);

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
                using (SqlCommand cmd = new SqlCommand("spMarcarAutobusDisponible", conexion.GetConnection()))
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
                using (SqlCommand cmd = new SqlCommand("spMarcarAutobusNoDisponible", conexion.GetConnection()))
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
