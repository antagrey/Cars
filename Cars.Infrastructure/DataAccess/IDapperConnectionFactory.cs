namespace Cars.Infrastructure.DataAccess
{
    public interface IDapperConnectionFactory
    {
        IDapperConnection CreateConnection(string connectionString);
    }
}
