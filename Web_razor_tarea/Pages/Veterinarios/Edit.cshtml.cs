using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Veterinarios
{
    public class EditModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public EditModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Veterinario Veterinario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null) return NotFound();

            Veterinario = veterinario;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Veterinario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Veterinarios.Any(v => v.Id == Veterinario.Id))
                    return NotFound();
                throw;
            }

            TempData["Exito"] = $"Veterinario '{Veterinario.Nombre} {Veterinario.Apellidos}' actualizado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}
