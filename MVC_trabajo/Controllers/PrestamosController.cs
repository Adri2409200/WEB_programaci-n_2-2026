using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_trabajo.Data;
using MVC_trabajo.Filters;
using MVC_trabajo.Models;

namespace MVC_trabajo.Controllers
{
    // Todos los logueados acceden, pero con restricciones internas
    [SoloLogueado]
    public class PrestamosController : Controller
    {
        private readonly AppDbContext _context;

        public PrestamosController(AppDbContext context)
        {
            _context = context;
        }

        // Admin y Bibliotecario ven todos los préstamos
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Index()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .ToListAsync();
            return View(prestamos);
        }

        // El usuario normal solo ve sus propios préstamos
        public async Task<IActionResult> MisPrestamos()
        {
            var idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
                return RedirectToAction("Login", "Account");

            var misPrestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Where(p => p.UsuarioId == idUsuario)
                .ToListAsync();

            return View(misPrestamos);
        }

        // Ver detalle de un préstamo
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestamo == null) return NotFound();

            // El usuario normal solo puede ver sus propios préstamos
            var rol = HttpContext.Session.GetString("UsuarioRol");
            var idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (rol == "Usuario" && prestamo.UsuarioId != idUsuario)
                return RedirectToAction("MisPrestamos");

            return View(prestamo);
        }

        // Solo Admin y Bibliotecario pueden crear préstamos
        [SoloLogueado("Administrador", "Bibliotecario")]
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,LibroId,FechaPrestamo,FechaDevolucion,Estado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.UsuarioId);
            return View(prestamo);
        }

        // Solo Admin y Bibliotecario pueden editar préstamos
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null) return NotFound();

            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.UsuarioId);
            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,LibroId,FechaPrestamo,FechaDevolucion,Estado")] Prestamo prestamo)
        {
            if (id != prestamo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo", prestamo.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.UsuarioId);
            return View(prestamo);
        }

        // Solo Admin puede eliminar préstamos
        [SoloLogueado("Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestamo == null) return NotFound();
            return View(prestamo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
                _context.Prestamos.Remove(prestamo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
