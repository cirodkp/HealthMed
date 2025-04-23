using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Domain.Validations
{
  public   class ConsultaValidator
    {
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        public static bool IsValidStatus(string status)
        {
            return !string.IsNullOrWhiteSpace(status);
        }
        public static bool IsValidNome(string Nome)
        {
            return !string.IsNullOrWhiteSpace(Nome);
        }

        public static bool IsValidCRM(string crm)
        {
            return !string.IsNullOrWhiteSpace(crm);
        }
    }
}
