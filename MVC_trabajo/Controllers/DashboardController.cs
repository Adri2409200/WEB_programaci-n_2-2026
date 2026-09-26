using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_trabajo.Data;
using MVC_trabajo.Filters;

namespace MVC_trabajo.Controllers
{
    [SoloLogueado]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            var idUsuario = HttpContext.Session.GetInt32("UsuarioId");

            if (string.IsNullOrEmpty(rol))
                return RedirectToAction("Login", "Account");

            // Datos generales para Admin y Bibliotecario
            ViewBag.TotalLibros       = await _db.Libros.CountAsync();
            ViewBag.TotalUsuarios     = await _db.Usuarios.CountAsync();
            ViewBag.TotalPrestamos    = await _db.Prestamos.CountAsync();
            ViewBag.PrestamosPendientes = await _db.Prestamos.CountAsync(p => p.Estado == "Pendiente");
            ViewBag.PrestamosVencidos   = await _db.Prestamos.CountAsync(p => p.Estado == "Vencido");
            ViewBag.PrestamosDevueltos  = await _db.Prestamos.CountAsync(p => p.Estado == "Devuelto");

            // Último usuario creado
            var ultimoUsuario = await _db.Usuarios.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
            ViewBag.UltimoUsuario = ultimoUsuario?.Nombre ?? "-";
            ViewBag.UltimoUsuarioRol = ultimoUsuario?.Rol ?? "-";

            // Último libro agregado
            var ultimoLibro = await _db.Libros.OrderByDescending(l => l.Id).FirstOrDefaultAsync();
            ViewBag.UltimoLibro = ultimoLibro?.Titulo ?? "-";
            ViewBag.UltimoLibroAutor = ultimoLibro?.Autor ?? "-";

            // Último préstamo registrado
            var ultimoPrestamo = await _db.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();
            ViewBag.UltimoPrestamo      = ultimoPrestamo?.Libro?.Titulo ?? "-";
            ViewBag.UltimoPrestamoUser  = ultimoPrestamo?.Usuario?.Nombre ?? "-";
            ViewBag.UltimoPrestamoFecha = ultimoPrestamo?.FechaPrestamo.ToString("dd/MM/yyyy") ?? "-";

            // Libros sin stock
            ViewBag.LibrosSinStock = await _db.Libros.CountAsync(l => l.Cantidad == 0);

            // Para el usuario normal: solo sus préstamos
            if (rol == "Usuario" && idUsuario.HasValue)
            {
                ViewBag.MisPrestamos         = await _db.Prestamos.CountAsync(p => p.UsuarioId == idUsuario);
                ViewBag.MisPendientes        = await _db.Prestamos.CountAsync(p => p.UsuarioId == idUsuario && p.Estado == "Pendiente");
                ViewBag.MisVencidos          = await _db.Prestamos.CountAsync(p => p.UsuarioId == idUsuario && p.Estado == "Vencido");

                var miUltimoPrestamo = await _db.Prestamos
                    .Include(p => p.Libro)
                    .Where(p => p.UsuarioId == idUsuario)
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();
                ViewBag.MiUltimoLibro  = miUltimoPrestamo?.Libro?.Titulo ?? "Ninguno aún";
                ViewBag.MiUltimaFecha  = miUltimoPrestamo?.FechaPrestamo.ToString("dd/MM/yyyy") ?? "-";
            }

            return rol switch
            {
                "Administrador" => View("DashboardAdmin"),
                "Bibliotecario" => View("DashboardBibliotecario"),
                _               => View("DashboardUsuario")
            };
        }
    }
}
