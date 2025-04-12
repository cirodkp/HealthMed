using static Dapper.SqlMapper;

namespace HealthMed.Domain.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<TEntity>> ExecuteQuery<TEntity>(string query);
    }
}