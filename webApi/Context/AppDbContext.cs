using Microsoft.EntityFrameworkCore;
using webApi.Features.Models;

namespace webApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        public DbSet<ControleTributo> ControleTributos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("cliente");

            modelBuilder.Entity<Empresa>().ToTable("empresa");

            modelBuilder.Entity<ControleTributo>().ToTable("controletributos");

            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.Cliente)
                .WithMany()
                .HasForeignKey(e => e.ClienteID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}