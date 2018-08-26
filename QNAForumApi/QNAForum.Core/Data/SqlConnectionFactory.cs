using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace QNAForum.Core.Data
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        private readonly string DataExceptionMessage = "SQL Server Exception:{0}";

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T WithConnection<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    return getData(con);
                };
            }
            catch (Exception ex)
            {
                throw new DataException(string.Format(DataExceptionMessage, ex.Message), ex);
            }

        }

        public async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    return await getData(con);
                };
            }
            catch (Exception ex)
            {
                throw new DataException(string.Format(DataExceptionMessage, ex.Message), ex);
            }

        }
    }
}