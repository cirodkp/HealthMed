using HealthMed.Doctor.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do appsettings
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Controllers
builder.Services.AddControllers();

// Swagger com metadados e suporte a JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HealthMed.Doctor.API",
        Version = "v1",
        Description = "API para gerenciamento de médicos no sistema HealthMed"
    });

    // Suporte a autenticação via JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira o token no formato: Bearer {seu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Injeções (IoC) para Repos, UseCases, JWT, MassTransit, OpenTelemetry
builder.Services.AddInjections(configuration);

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthMed.Doctor.API v1");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // mostra detalhes no navegador
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Prometheus
 app.MapPrometheusScrapingEndpoint();

// HTTPS e Auth
app.UseHttpsRedirection();

app.UseAuthentication(); // ⚠️ Sempre antes do Authorization
app.UseAuthorization();

// Rotas
app.MapControllers();

app.Run();
