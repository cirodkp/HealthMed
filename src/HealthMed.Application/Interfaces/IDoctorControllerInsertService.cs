using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorControllerInsertService
    {
        Task<SendResponseAsync> Execute(DoctorControllerInsertRequest insertDoctorRequest);
    }
}
