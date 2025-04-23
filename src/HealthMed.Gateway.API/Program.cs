using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using MMLib.SwaggerForOcelot.DependencyInjection;
using MMLib.SwaggerForOcelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.SwaggerEndPoints.json", optional: false, reloadOnChange: true);

// ✅ APENAS essas chamadas
builder.Services.AddOcelot();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseCors();

//app.UseSwaggerForOcelotUI(opt =>
//{
//    opt.PathToSwaggerGenerator = "/swagger/docs";
//});

await app.UseOcelot();
app.MapControllers();
app.Run();
