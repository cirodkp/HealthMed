using HealthMed.Application.Events;

namespace HealthMed.Application.Results
{
    public class DoctorAgendaGetListEventResponse
    {
        public List<DoctorAgendaGetEventResponse> Items { get; set; } = [];
    }
}
