using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;

namespace ControlAutobuses.Datos
{
    public sealed class ConexionBD
    {
        private static ConexionBD instance = null;
        private static readonly object padlock = new object();
        private NpgsqlConnection connection;

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ControlAutobusesConnection"].ConnectionString;

        private ConexionBD()
        {
            connection = new NpgsqlConnection(connectionString);
        }

        public static ConexionBD Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConexionBD();
                    }
                    return instance;
                }
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        // Método adicional para verificar el estado de la conexión
        public bool TestConnection()
        {
            try
            {
                OpenConnection();
                CloseConnection();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
