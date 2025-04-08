using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IInsertDoctorUseCase
    {
        Task<PublishResponse> Execute(InsertDoctorRequest insertDoctorRequest);
    }
}
