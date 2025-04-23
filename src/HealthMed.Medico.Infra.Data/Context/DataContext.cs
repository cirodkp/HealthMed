 
using HealthMed.Doctor.Domain.Core;
using HealthMed.Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Doctor.Infra.Data.Context
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Especialidade> Especialidades => Set<Especialidade>();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medico>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Medico>()
                .HasMany(m => m.Horarios)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HorarioDisponivel>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<Especialidade>()
                .HasKey(e => e.EspecialidadeId);


            modelBuilder.Entity<Medico>(ConfigureMedico);
            modelBuilder.Entity<Especialidade>(ConfigureEspecialidade);
        }

        private void ConfigureMedico(EntityTypeBuilder<Medico> entity)
        {
            entity.ToTable("medicos");

            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).HasColumnName("id").HasColumnType("uuid");
            entity.Property(m => m.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
            entity.Property(m => m.CRM).HasColumnName("crm").HasMaxLength(50).IsRequired();
            entity.Property(m => m.Especialidade).HasColumnName("especialidade").HasMaxLength(100).IsRequired();
        }

        private void ConfigureEspecialidade(EntityTypeBuilder<Especialidade> entity)
        {
            entity.ToTable("especialidades"); // Nome da tabela

            entity.HasKey(e => e.EspecialidadeId);
            entity.Property(e => e.EspecialidadeId).HasColumnName("id").HasColumnType("uuid");
            entity.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(255).IsRequired();
            entity.Property(e => e.Categoria).HasColumnName("categoria").HasMaxLength(100).IsRequired();
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
