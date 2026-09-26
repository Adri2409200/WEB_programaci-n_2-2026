using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_trabajo.Data;
using MVC_trabajo.Filters;
using MVC_trabajo.Models;

namespace MVC_trabajo.Controllers
{
    // Todos los logueados pueden ver libros
    [SoloLogueado]
    public class LibrosController : Controller
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        // Todos pueden ver el catálogo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libros.ToListAsync());
        }

        // Todos pueden ver el detalle de un libro
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // Solo Admin y Bibliotecario pueden crear libros
        [SoloLogueado("Administrador", "Bibliotecario")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,ISBN,Editorial,Anio,Categoria,Cantidad")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // Solo Admin y Bibliotecario pueden editar libros
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador", "Bibliotecario")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,ISBN,Editorial,Anio,Categoria,Cantidad")] Libro libro)
        {
            if (id != libro.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // Solo Admin puede eliminar libros
        [SoloLogueado("Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SoloLogueado("Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
                _context.Libros.Remove(libro);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
