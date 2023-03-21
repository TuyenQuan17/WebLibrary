using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaziLibrary.DataAccess.Oracle
{
    public static class Context
    {
        public static OracleConnection Connection()
        {
            var connectionString = "User Id = hr;Password = hr;Data Source = localhost:1521/ORCL;";
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
