using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Propietarios
{
    public class EditModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public EditModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Propietario Propietario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(m => m.Id == id);
            if (propietario == null) return NotFound();

            Propietario = propietario;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Propietario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Propietarios.Any(e => e.Id == Propietario.Id))
                    return NotFound();
                throw;
            }

            TempData["Exito"] = $"Propietario '{Propietario.Nombre} {Propietario.Apellidos}' actualizado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}
