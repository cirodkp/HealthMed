using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAdminControllerService
    {
        Task<SendResponseAsync> Execute(DoctorAdminControllerInsertRequest insertDoctorRequest);
    }
}
