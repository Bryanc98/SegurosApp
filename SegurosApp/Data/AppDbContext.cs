using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SegurosApp.Models;

namespace SegurosApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ProductoFinanciero> ProductoFinancieros { get; set; }
        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Cliente_ProductoFinanciero> Cliente_ProductoFinancieros { get; set; }
        public DbSet<Seguro_ProductoFinanciero> Seguro_ProductoFinancieros { get; set; }
        public DbSet<Cliente_Seguro> Cliente_Seguros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Cliente>()
        .HasKey(c => c.ID);

            modelBuilder.Entity<ProductoFinanciero>()
                .HasKey(pf => pf.Id);

            modelBuilder.Entity<Seguro>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Plan>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Cliente_ProductoFinanciero>()
                .HasKey(cp => cp.ID);

            modelBuilder.Entity<Seguro_ProductoFinanciero>()
                .HasKey(sp => sp.Id);

            modelBuilder.Entity<Cliente_Seguro>()
                .HasKey(cs => cs.ID);

            // Relaciones
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Seguro)
                .WithMany()
                .HasForeignKey(p => p.SeguroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente_ProductoFinanciero>()
            .HasOne(cpf => cpf.Cliente)
            .WithMany(c => c.Cliente_ProductoFinancieros)
            .HasForeignKey(cpf => cpf.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente_ProductoFinanciero>()
                .HasOne(cp => cp.ProductoFinanciero)
                .WithMany()
                .HasForeignKey(cp => cp.ProductoFinancieroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seguro_ProductoFinanciero>()
                .HasOne(sp => sp.Seguro)
                .WithMany()
                .HasForeignKey(sp => sp.SeguroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seguro_ProductoFinanciero>()
                .HasOne(sp => sp.ProductoFinanciero)
                .WithMany()
                .HasForeignKey(sp => sp.ProductoFinancieroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente_Seguro>()
                .HasOne(cs => cs.Cliente)
                .WithMany()
                .HasForeignKey(cs => cs.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente_Seguro>()
            .HasOne(cs => cs.Cliente)
            .WithMany(c => c.Cliente_Seguros)
            .HasForeignKey(cs => cs.ClienteID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente_Seguro>()
                .HasOne(cs => cs.Plan)
                .WithMany()
                .HasForeignKey(cs => cs.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente_Seguro>()
                .HasOne(cs => cs.ProductoFinanciero)
                .WithMany()
                .HasForeignKey(cs => cs.ProductoFinancieroId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Plan>()
                .Property(p => p.couta)
                .HasColumnType("decimal(18,2)");

        }


    }
}
