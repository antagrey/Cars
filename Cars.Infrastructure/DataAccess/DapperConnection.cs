using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace Cars.Infrastructure.DataAccess
{
    public class DapperConnection : IDapperConnection
    {
        private readonly IDbConnection connection;
        private bool disposed;

        public DapperConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            if (connection.State != ConnectionState.Open) connection.Open();
        }

        public Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            CommandType commandType,
            object param = null)
        {
            return connection.QueryAsync<T>(sql, param, null, null, commandType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
                if (connection.State != ConnectionState.Closed)
                    connection.Dispose();

            disposed = true;
        }
    }
}
