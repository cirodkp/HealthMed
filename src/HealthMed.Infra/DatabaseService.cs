using Npgsql;

namespace HealthMed.Infra
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ValidateDoctorCredentialsAsync(string crm, string password)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("SELECT COUNT(1) FROM medicos WHERE crm = @crm AND pass_hash = @password", connection);
            command.Parameters.AddWithValue("crm", crm);
            command.Parameters.AddWithValue("password", password);

            var result = await command.ExecuteScalarAsync();

            return (long)result > 0;
        }
    }
}
