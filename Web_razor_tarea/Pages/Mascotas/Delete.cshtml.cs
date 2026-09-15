using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Mascotas
{
    public class DeleteModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DeleteModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Mascota Mascota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var mascota = await _context.Mascotas
                .Include(m => m.Propietario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mascota == null) return NotFound();

            Mascota = mascota;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Mascota '{mascota.Nombre}' eliminada.";
            }

            return RedirectToPage("./Index");
        }
    }
}
