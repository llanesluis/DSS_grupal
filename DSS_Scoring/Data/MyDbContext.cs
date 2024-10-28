using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options): base(options){}

    public DbSet<Prueba> Pruebas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prueba>(entity =>
        {
            // Configuración personalizada de 'Prueba' si es necesario
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
