using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Datos
{
    public class AsignacionRepository
    {
        private readonly ConexionBD conexion;

        public AsignacionRepository()
        {
            conexion = ConexionBD.Instance;
        }

        public bool Crear(Asignacion asignacion)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spCrearAsignacion", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ChoferId", asignacion.ChoferId);
                    cmd.Parameters.AddWithValue("@AutobusId", asignacion.AutobusId);
                    cmd.Parameters.AddWithValue("@RutaId", asignacion.RutaId);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<Asignacion> ObtenerTodas()
        {
            var asignaciones = new List<Asignacion>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerTodasAsignaciones", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            asignaciones.Add(new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"]),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                PlacaAutobus = reader["PlacaAutobus"].ToString(),
                                NombreRuta = reader["NombreRuta"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return asignaciones;
        }

        public List<Asignacion> ObtenerActivas()
        {
            var asignaciones = new List<Asignacion>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAsignacionesActivas", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            asignaciones.Add(new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"]),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                PlacaAutobus = reader["PlacaAutobus"].ToString(),
                                NombreRuta = reader["NombreRuta"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return asignaciones;
        }

        public bool Finalizar(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spFinalizarAsignacion", conexion.GetConnection()))
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

        public Asignacion ObtenerPorId(int id)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAsignacionPorId", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"])
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

        public List<Asignacion> ObtenerPorChofer(int choferId)
        {
            var asignaciones = new List<Asignacion>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAsignacionesPorChofer", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ChoferId", choferId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            asignaciones.Add(new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"]),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                PlacaAutobus = reader["PlacaAutobus"].ToString(),
                                NombreRuta = reader["NombreRuta"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return asignaciones;
        }

        public List<Asignacion> ObtenerPorAutobus(int autobusId)
        {
            var asignaciones = new List<Asignacion>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAsignacionesPorAutobus", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AutobusId", autobusId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            asignaciones.Add(new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"]),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                PlacaAutobus = reader["PlacaAutobus"].ToString(),
                                NombreRuta = reader["NombreRuta"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return asignaciones;
        }

        public List<Asignacion> ObtenerPorRuta(int rutaId)
        {
            var asignaciones = new List<Asignacion>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerAsignacionesPorRuta", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RutaId", rutaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            asignaciones.Add(new Asignacion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                ChoferId = Convert.ToInt32(reader["ChoferId"]),
                                AutobusId = Convert.ToInt32(reader["AutobusId"]),
                                RutaId = Convert.ToInt32(reader["RutaId"]),
                                FechaAsignacion = Convert.ToDateTime(reader["FechaAsignacion"]),
                                Activa = Convert.ToBoolean(reader["Activa"]),
                                NombreChofer = reader["NombreChofer"].ToString(),
                                PlacaAutobus = reader["PlacaAutobus"].ToString(),
                                NombreRuta = reader["NombreRuta"].ToString()
                            });
                        }
                    }
                }
            }
            finally
            {
                conexion.CloseConnection();
            }

            return asignaciones;
        }

        public bool ChoferTieneAsignacionActiva(int choferId)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spChoferTieneAsignacionActiva", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ChoferId", choferId);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public bool AutobusTieneAsignacionActiva(int autobusId)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spAutobusTieneAsignacionActiva", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AutobusId", autobusId);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public bool RutaTieneAsignacionActiva(int rutaId)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spRutaTieneAsignacionActiva", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RutaId", rutaId);

                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}