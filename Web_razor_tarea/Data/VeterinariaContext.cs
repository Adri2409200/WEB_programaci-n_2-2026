using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Data;

public class VeterinariaContext : DbContext
{
    public VeterinariaContext(DbContextOptions<VeterinariaContext> opciones)
        : base(opciones) { }

    // Tablas de la base de datos
    public DbSet<Propietario> Propietarios { get; set; }
    public DbSet<Mascota>     Mascotas     { get; set; }
    public DbSet<Veterinario> Veterinarios { get; set; }
    public DbSet<Cita>        Citas        { get; set; }

    protected override void OnModelCreating(ModelBuilder modelo)
    {
        base.OnModelCreating(modelo);

        // Nombres de tablas en español
        modelo.Entity<Propietario>().ToTable("Propietarios");
        modelo.Entity<Mascota>().ToTable("Mascotas");
        modelo.Entity<Veterinario>().ToTable("Veterinarios");
        modelo.Entity<Cita>().ToTable("Citas");

        // El email del propietario debe ser único
        modelo.Entity<Propietario>()
            .HasIndex(p => p.Email)
            .IsUnique();

        // Una mascota pertenece a un propietario; si se borra el propietario, se restringe
        modelo.Entity<Mascota>()
            .HasOne(m => m.Propietario)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(m => m.PropietarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Una cita referencia a una mascota; restricción al borrar
        modelo.Entity<Cita>()
            .HasOne(c => c.Mascota)
            .WithMany(m => m.Citas)
            .HasForeignKey(c => c.MascotaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Una cita referencia a un veterinario; restricción al borrar
        modelo.Entity<Cita>()
            .HasOne(c => c.Veterinario)
            .WithMany(v => v.Citas)
            .HasForeignKey(c => c.VeterinarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Guardar el enum EstadoCita como texto en la BD
        modelo.Entity<Cita>()
            .Property(c => c.Estado)
            .HasConversion<string>();
    }
}
