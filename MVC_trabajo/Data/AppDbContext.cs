using Microsoft.EntityFrameworkCore;
using MVC_trabajo.Models;

namespace MVC_trabajo.Data
{
    // Contexto principal de la base de datos
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opciones) : base(opciones)
        {
        }

        // Tabla de usuarios del sistema
        public DbSet<Usuario> Usuarios { get; set; }

        // Tabla de libros de la biblioteca
        public DbSet<Libro> Libros { get; set; }

        // Tabla de préstamos realizados
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacion: un usuario puede tener muchos préstamos
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Prestamos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacion: un libro puede tener muchos préstamos
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Libro)
                .WithMany(l => l.Prestamos)
                .HasForeignKey(p => p.LibroId)
                .OnDelete(DeleteBehavior.Cascade);

            // El correo del usuario debe ser único
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            // El ISBN del libro debe ser único
            modelBuilder.Entity<Libro>()
                .HasIndex(l => l.ISBN)
                .IsUnique();
        }
    }
}
