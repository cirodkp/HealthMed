using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IPatientControllerInsertService
    {
        Task<SendResponseAsync> Execute(PatientControllerInsertRequest insertPatientRequest);
    }
}
