using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Citas
{
    public class DetailsModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DetailsModel(VeterinariaContext context)
        {
            _context = context;
        }

        public Cita Cita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Carga la cita con mascota y veterinario
            var cita = await _context.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null) return NotFound();

            Cita = cita;
            return Page();
        }
    }
}
