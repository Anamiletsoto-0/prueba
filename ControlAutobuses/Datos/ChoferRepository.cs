using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Datos
{
    public class ChoferRepository
    {
        private readonly ConexionBD conexion;

        public ChoferRepository()
        {
            conexion = ConexionBD.Instance;
        }

        public bool Crear(Chofer chofer)
        {
            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spCrearChofer", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", chofer.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", chofer.Apellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Cedula", chofer.Cedula);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<Chofer> ObtenerTodos()
        {
            var choferes = new List<Chofer>();

            try
            {
                conexion.OpenConnection();
                using (SqlCommand cmd = new SqlCommand("spObtenerChoferes", conexion.GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            choferes.Add(new Chofer
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Cedula = reader["Cedula"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
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

            return choferes;
        }

        // Implementar métodos para Actualizar, Eliminar, etc.
    }
}
