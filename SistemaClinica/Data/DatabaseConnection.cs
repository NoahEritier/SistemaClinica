using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SistemaClinica.Data
{
    public static class DatabaseConnection
    {
        public static MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(Configuration.ConnectionString);
            connection.Open();
            return connection;
        }
    }
}