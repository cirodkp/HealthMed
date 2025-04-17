using HealthMed.Domain.Entities;
using HealthMed.Infra.Models;
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

        public async Task<DoctorDbValidationResult> ValidateDoctorCredentialsAsync(string crm, string password)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("SELECT nome FROM medicos WHERE crm = @crm AND pass_hash = @password", connection);
            command.Parameters.AddWithValue("crm", crm);
            command.Parameters.AddWithValue("password", password);

            var result = await command.ExecuteScalarAsync();

            if (result != null && result is string name)
            {
                return new DoctorDbValidationResult
                {
                    Found = true,
                    Name = name
                };
            }

            return new DoctorDbValidationResult
            {
                Found = false,
                ErrorMessage = "CRM ou senha inválidos"
            };
        }

        public async Task<bool> InsertDoctorAgendaAsync(DoctorAgendaInsert doctorAgenda)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var getIdCommand = new NpgsqlCommand("SELECT id FROM medicos WHERE crm = @crm LIMIT 1", connection);
            getIdCommand.Parameters.AddWithValue("crm", doctorAgenda.Crm);

            var medicoIdObj = await getIdCommand.ExecuteScalarAsync();
            if (medicoIdObj == null)
            {
                Console.WriteLine($"Médico com CRM {doctorAgenda.Crm} não encontrado.");
                return false;
            }

            var medicoId = Convert.ToInt32(medicoIdObj);

            var insertAgendaCommand = new NpgsqlCommand(@"
                INSERT INTO medicos_agenda (id_medico, data_agenda, hora_agenda) 
                VALUES (@id_medico, @data_agenda, @hora_agenda)", connection);

            insertAgendaCommand.Parameters.AddWithValue("id_medico", medicoId);
            insertAgendaCommand.Parameters.AddWithValue("data_agenda", doctorAgenda.DataHoraAgenda.Date);
            insertAgendaCommand.Parameters.AddWithValue("hora_agenda", doctorAgenda.DataHoraAgenda.TimeOfDay);


            var result = await insertAgendaCommand.ExecuteNonQueryAsync();

            return result > 0;
        }

        public async Task<IEnumerable<DoctorAgendaGet>> GetDoctorAgendasByCrmAsync(string crm)
        {
            
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var agendas = new List<DoctorAgendaGet>();

            // 1. Buscar o ID do médico
            var getIdCommand = new NpgsqlCommand("SELECT id FROM medicos WHERE crm = @crm LIMIT 1", connection);
            getIdCommand.Parameters.AddWithValue("crm", crm);

            var medicoIdObj = await getIdCommand.ExecuteScalarAsync();
            if (medicoIdObj == null)
            {
                Console.WriteLine($"Médico com CRM {crm} não encontrado.");
                return agendas;
            }

            var medicoId = Convert.ToInt32(medicoIdObj);

            // 2. Buscar agendas do médico
            var selectAgendasCommand = new NpgsqlCommand(@"
                SELECT data_agenda, hora_agenda
                FROM medicos_agenda
                WHERE id_medico = @id_medico
                ORDER BY data_agenda, hora_agenda", connection);

            selectAgendasCommand.Parameters.AddWithValue("id_medico", medicoId);

            await using var reader = await selectAgendasCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var data = reader.GetDateTime(0); // data_agenda
                var hora = reader.GetTimeSpan(1); // hora_agenda

                agendas.Add(new DoctorAgendaGet
                {
                    Crm = crm,
                    DataHoraAgenda = data.Date + hora,
                    IsScheduled = true // ou false se quiser indicar livre/ocupado
                });
            }

            return agendas;
        }
    }
}
