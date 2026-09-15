using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Veterinarios
{
    public class DeleteModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DeleteModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Veterinario Veterinario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var veterinario = await _context.Veterinarios.FirstOrDefaultAsync(v => v.Id == id);
            if (veterinario == null) return NotFound();

            Veterinario = veterinario;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinarios.Remove(veterinario);
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Veterinario '{veterinario.Nombre} {veterinario.Apellidos}' eliminado.";
            }

            return RedirectToPage("./Index");
        }
    }
}
