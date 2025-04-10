using System.Data;
using System.Threading.Tasks;

namespace HealthMed.Infra
{
    public interface IDatabaseService
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
