using HealthMed.Application.Events;
using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IInsertDoctorService
    {
        Task<PublishAsyncResponse> Execute(InsertDoctorRequest insertDoctorRequest);
    }
}
