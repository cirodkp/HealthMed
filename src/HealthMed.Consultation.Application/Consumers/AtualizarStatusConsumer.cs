using HealthMed.Consultation.Application.Events;
using HealthMed.Consultation.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Application.Consumers
{
    public class AtualizarStatusConsumer : IConsumer<AtualizarStatusEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        public AtualizarStatusConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Consume(ConsumeContext<AtualizarStatusEvent> context)
        {
            var message = context.Message;

            try
            {
                // Grava status no DB
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                        .GetRequiredService<IConsultaRepository>();

                    var Consulta = await scopedProcessingService.ObterPorIdAsync(message.Id);

                    Consulta.Update(message.Id, message.Status , message.Justificativa, Consulta.DataHora);
                    scopedProcessingService.AtualizarStatusAsync(Consulta);
                    await scopedProcessingService.UnitOfWork.Commit();

                 

                    Console.WriteLine($"Consulta atualizado com sucesso: {Consulta.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: Consulta:{message.Id} {ex.Message}");
            }
        }
    }
}
