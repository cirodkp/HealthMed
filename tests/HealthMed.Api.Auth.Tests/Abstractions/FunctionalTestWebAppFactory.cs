using HealthMed.Auth.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

namespace HealthMed.Api.Auth.Tests.Abstractions
{
    public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //var contextOptions = services.SingleOrDefault(
                //    d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                services.Remove(services.Single(a => typeof(DbContextOptions<DataContext>) == a.ServiceType));
                services.AddDbContext<DataContext>(options => options
                    .UseNpgsql(_postgreSqlContainer.GetConnectionString()));

               
                RunScriptDatabase(services);
            });
        }

        private void RunScriptDatabase(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var dataContext = serviceProvider.GetRequiredService<DataContext>();
            dataContext.Database.EnsureCreated();
        }
        public async Task InitializeAsync()
        {
            await _postgreSqlContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _postgreSqlContainer.StopAsync();
        }
    }
}