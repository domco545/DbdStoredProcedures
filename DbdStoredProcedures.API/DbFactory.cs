using System.Data;
using System.Data.SqlClient;

namespace DbdStoredProcedures.API;

public class DbFactory
{
    private readonly string _connectionString;

    public DbFactory(IConfiguration configuration)
    {
        _connectionString = 
            configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Database connection string not found.");
    }
    
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}