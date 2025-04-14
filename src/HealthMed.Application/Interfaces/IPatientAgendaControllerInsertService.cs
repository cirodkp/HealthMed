using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IPatientAgendaControllerInsertService
    {
        Task<SendResponseAsync> Execute(PatientAgendaControllerInsertRequest insertPatientAgendaRequest);
    }
}
