using Dapper;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using System.Data;


namespace HealthMed.Domain.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        private readonly string _table;

        private static readonly Dictionary<Type, string> TableMap = new()
        {
            { typeof(Patient), "patient" },
            { typeof(Doctor), "doctor" },
            { typeof(DoctorSchedule), "doctor_schedule" },
            { typeof(Appointment), "appointment" },
        };

        public GenericRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
            if (!TableMap.TryGetValue(typeof(T), out _table))
                throw new Exception($"No table mapping found for entity type {typeof(T).Name}");
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var query = $"SELECT * FROM {_table} WHERE id = @id";
            return await _connection.QueryFirstOrDefaultAsync<T>(query, new { id }, _transaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_table}";
            return await _connection.QueryAsync<T>(query, transaction: _transaction);
        }

        public async Task AddAsync(T entity)
        {

            var query = "INSERT INTO {_table} ({columns}) VALUES ({values})";
            await _connection.ExecuteAsync(query, entity, _transaction);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            var query = "UPDATE {_table} SET {setClause} WHERE id = @Id";
            await _connection.ExecuteAsync(query, entity, _transaction);
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM {_table} WHERE id = @id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }
    }
}