using FluentAssertions;

namespace HealthMed.Agenda.Integration.Tests.Extensions
{
   
        public static class DateTimeAssertionsExtensions
        {
            public static void ShouldContainDateCloseTo(this IEnumerable<DateTime> lista, DateTime esperado, TimeSpan tolerancia)
            {
                var esperadoUtc = esperado.ToUniversalTime();

                lista.Any(data =>
                    data.ToUniversalTime() >= esperadoUtc - tolerancia &&
                    data.ToUniversalTime() <= esperadoUtc + tolerancia
                ).Should().BeTrue($"Esperava-se um DateTime dentro de {tolerancia.TotalSeconds} segundos de {esperadoUtc}.");
            }
        }
    
}
