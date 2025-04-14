using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAgendaControllerGetService
    {
        Task<IEnumerable<DoctorAgendaResponse>> Execute(DoctorAgendaControllerGetRequest request);
    }
}
