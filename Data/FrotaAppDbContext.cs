using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TccFrotaApp.Models;

namespace TccFrotaApp.Data
{
    public class FrotaAppDbContext : IdentityDbContext<Login>
    {
        public FrotaAppDbContext(DbContextOptions<FrotaAppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            modelBuilder.Entity<Colaborador>()
            .HasMany(b => b.ApontamentosMotorista)
            .WithOne(c => c.Motorista).OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Colaborador>()
            .HasMany(b => b.ApontamentosColetor1)
            .WithOne(c => c.Coletor1).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Colaborador>()
            .HasMany(b => b.ApontamentosColetor2)
            .WithOne(c => c.Coletor2).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Colaborador>()
            .HasMany(b => b.ApontamentosColetor3)
            .WithOne(c => c.Coletor3).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Veiculo>()
            .HasMany(b => b.Apontamentos)
            .WithOne(c => c.Veiculo).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Apontamento> Apontamentos { get; set; }
    }

    public class FrotaAppDbContextFactory : IDesignTimeDbContextFactory<FrotaAppDbContext>
    {
        public FrotaAppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<FrotaAppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new FrotaAppDbContext(builder.Options);
        }
    }
}


