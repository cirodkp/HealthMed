using HealthMed.Domain.Entities;
using Npgsql;

namespace HealthMed.Infra
{
    public class DoctorDatabaseService : IDoctorDatabaseService
    {
        private readonly string _connectionString;

        public DoctorDatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> InsertDoctorAsync(DoctorInsert doctor)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("INSERT INTO medicos (crm, email, pass_hash, key_MFA, nome) VALUES (@crm, @email, @pass_hash, @key_MFA, @nome)", connection);
            command.Parameters.AddWithValue("crm", doctor.Crm);
            command.Parameters.AddWithValue("email", doctor.Email);
            command.Parameters.AddWithValue("pass_hash", doctor.PasswordHash);
            command.Parameters.AddWithValue("key_MFA", doctor.KeyMFA);
            command.Parameters.AddWithValue("nome", doctor.Name);

            var result = await command.ExecuteNonQueryAsync();

            return (long)result > 0;
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
