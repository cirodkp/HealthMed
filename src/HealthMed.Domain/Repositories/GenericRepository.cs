using Dapper;
using HealthMed.Domain.Interfaces;
using Npgsql;
using System.Data;
using static Dapper.SqlMapper;


namespace HealthMed.Domain.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        public GenericRepository()
        {
                
        }

        public async Task<IEnumerable<TEntity>> ExecuteQuery<TEntity>(string query)
        {
            using (IDbConnection db = new NpgsqlConnection("databaseConnectionString"))
            {
                return await db.QueryAsync<TEntity>(query);
            }
        }
    }
}