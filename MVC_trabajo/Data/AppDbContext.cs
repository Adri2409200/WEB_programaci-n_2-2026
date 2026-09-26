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

            // Datos iniciales de usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nombre = "Administrador", Correo = "admin@gmail.com",    Contrasena = "123456", Rol = "Administrador" },
                new Usuario { Id = 2, Nombre = "Bibliotecario",  Correo = "biblio@gmail.com",  Contrasena = "123456", Rol = "Bibliotecario"  },
                new Usuario { Id = 3, Nombre = "Usuario Normal", Correo = "usuario@gmail.com", Contrasena = "123456", Rol = "Usuario"        }
            );

            // Datos iniciales de libros
            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = 1,  Titulo = "Percy Jackson y el ladrón del rayo",  Autor = "Rick Riordan",         ISBN = "F-01", Editorial = "Salamandra", Anio = 2006, Categoria = "Fantasía",    Cantidad = 56 },
                new Libro { Id = 2,  Titulo = "Alicia en el País de las Maravillas", Autor = "Lewis Carroll",        ISBN = "F-02", Editorial = "Anaya",      Anio = 2016, Categoria = "Fantasía",    Cantidad = 24 },
                new Libro { Id = 3,  Titulo = "El Principito",                       Autor = "Antoine de Saint-Exupéry", ISBN = "F-03", Editorial = "Salamandra", Anio = 1943, Categoria = "Clásico",    Cantidad = 30 },
                new Libro { Id = 4,  Titulo = "Cien años de soledad",                Autor = "Gabriel García Márquez",   ISBN = "L-01", Editorial = "Sudamericana", Anio = 1967, Categoria = "Literatura", Cantidad = 15 },
                new Libro { Id = 5,  Titulo = "Don Quijote de la Mancha",            Autor = "Miguel de Cervantes",  ISBN = "L-02", Editorial = "RAE",        Anio = 1605, Categoria = "Clásico",    Cantidad = 10 },
                new Libro { Id = 6,  Titulo = "Harry Potter y la piedra filosofal",  Autor = "J.K. Rowling",         ISBN = "F-04", Editorial = "Salamandra", Anio = 1997, Categoria = "Fantasía",    Cantidad = 40 },
                new Libro { Id = 7,  Titulo = "El código Da Vinci",                  Autor = "Dan Brown",            ISBN = "T-01", Editorial = "Umbriel",    Anio = 2003, Categoria = "Thriller",    Cantidad = 20 },
                new Libro { Id = 8,  Titulo = "Introducción a la Programación",      Autor = "John Zelle",           ISBN = "T-02", Editorial = "McGraw-Hill", Anio = 2010, Categoria = "Tecnología", Cantidad = 18 },
                new Libro { Id = 9,  Titulo = "Clean Code",                          Autor = "Robert C. Martin",     ISBN = "T-03", Editorial = "Prentice Hall", Anio = 2008, Categoria = "Tecnología", Cantidad = 12 },
                new Libro { Id = 10, Titulo = "El Señor de los Anillos",             Autor = "J.R.R. Tolkien",       ISBN = "F-05", Editorial = "Minotauro",  Anio = 1954, Categoria = "Fantasía",    Cantidad = 35 }
            );
        }
    }
}
