using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Mascotas
{
    public class DetailsModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DetailsModel(VeterinariaContext context)
        {
            _context = context;
        }

        public Mascota Mascota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Carga la mascota con su propietario
            var mascota = await _context.Mascotas
                .Include(m => m.Propietario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mascota == null) return NotFound();

            Mascota = mascota;
            return Page();
        }
    }
}
