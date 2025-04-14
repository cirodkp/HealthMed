using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAgendaControllerInsertService
    {
        Task<SendResponseAsync> Execute(DoctorAgendaControllerInsertRequest insertDoctorAgendaRequest);
    }
}
