using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IPatientAgendaControllerGetService
    {
        Task<IEnumerable<PatientAgendaResponse>> Execute(PatientAgendaControllerGetRequest request);
    }
}
