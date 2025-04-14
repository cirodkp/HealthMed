using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAgendaControllerDeleteService
    {
        Task<SendResponseAsync> Execute(DoctorAgendaControllerDeleteRequest deleteDoctorAgendaRequest);
    }
}
