using Microsoft.EntityFrameworkCore;
using PrioridadesApi.Models;

namespace PrioridadesApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Prioridad> Prioridades => Set<Prioridad>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prioridad>(e =>
        {
            e.Property(p => p.Titulo).IsRequired().HasMaxLength(150);
            e.Property(p => p.Nivel).HasConversion<int>();
            e.HasIndex(p => p.Nivel);
            e.HasData(
                new Prioridad { Id = 1, Titulo = "Entregar tarea Aplicada 1", Nivel = NivelPrioridad.Alta, Completada = false, CreadaEl = DateTime.UtcNow },
                new Prioridad { Id = 2, Titulo = "Hacer backup del proyecto", Nivel = NivelPrioridad.Media, Completada = false, CreadaEl = DateTime.UtcNow }
            );
        });
    }
}
