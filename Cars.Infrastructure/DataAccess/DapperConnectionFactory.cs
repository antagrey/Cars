namespace Cars.Infrastructure.DataAccess
{
    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        public IDapperConnection CreateConnection(string connectionString)
        {
            return new DapperConnection(connectionString);
        }
    }
}
