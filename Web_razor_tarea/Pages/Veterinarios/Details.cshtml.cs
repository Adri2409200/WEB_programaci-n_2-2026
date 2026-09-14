using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Veterinarios
{
    public class DetailsModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DetailsModel(VeterinariaContext context)
        {
            _context = context;
        }

        public Veterinario Veterinario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var veterinario = await _context.Veterinarios
                .FirstOrDefaultAsync(v => v.Id == id);

            if (veterinario == null) return NotFound();

            Veterinario = veterinario;
            return Page();
        }
    }
}
