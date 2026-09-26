using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_trabajo.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "Anio", "Autor", "Cantidad", "Categoria", "Editorial", "ISBN", "Titulo" },
                values: new object[,]
                {
                    { 1, 2006, "Rick Riordan", 56, "Fantasía", "Salamandra", "F-01", "Percy Jackson y el ladrón del rayo" },
                    { 2, 2016, "Lewis Carroll", 24, "Fantasía", "Anaya", "F-02", "Alicia en el País de las Maravillas" },
                    { 3, 1943, "Antoine de Saint-Exupéry", 30, "Clásico", "Salamandra", "F-03", "El Principito" },
                    { 4, 1967, "Gabriel García Márquez", 15, "Literatura", "Sudamericana", "L-01", "Cien años de soledad" },
                    { 5, 1605, "Miguel de Cervantes", 10, "Clásico", "RAE", "L-02", "Don Quijote de la Mancha" },
                    { 6, 1997, "J.K. Rowling", 40, "Fantasía", "Salamandra", "F-04", "Harry Potter y la piedra filosofal" },
                    { 7, 2003, "Dan Brown", 20, "Thriller", "Umbriel", "T-01", "El código Da Vinci" },
                    { 8, 2010, "John Zelle", 18, "Tecnología", "McGraw-Hill", "T-02", "Introducción a la Programación" },
                    { 9, 2008, "Robert C. Martin", 12, "Tecnología", "Prentice Hall", "T-03", "Clean Code" },
                    { 10, 1954, "J.R.R. Tolkien", 35, "Fantasía", "Minotauro", "F-05", "El Señor de los Anillos" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasena", "Correo", "Nombre", "Rol" },
                values: new object[,]
                {
                    { 1, "123456", "admin@gmail.com", "Administrador", "Administrador" },
                    { 2, "123456", "biblio@gmail.com", "Bibliotecario", "Bibliotecario" },
                    { 3, "123456", "usuario@gmail.com", "Usuario Normal", "Usuario" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
