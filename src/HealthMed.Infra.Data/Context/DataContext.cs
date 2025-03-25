using HealthMed.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorCredentials>();
        }
    }
}
