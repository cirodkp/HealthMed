using HealthMed.Consumer;
using HealthMed.Consumer.IoC;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInjections(builder.Configuration);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransitHostedService();

var host = builder.Build();
await host.RunAsync();





