using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Propietarios
{
    public class DeleteModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public DeleteModel(VeterinariaContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var propietario = await _context.Propietarios.FindAsync(id);
            if (propietario != null)
            {
                _context.Propietarios.Remove(propietario);
                await _context.SaveChangesAsync();
                TempData["Exito"] = $"Propietario '{propietario.Nombre} {propietario.Apellidos}' eliminado.";
            }

            return RedirectToPage("./Index");
        }
    }
}
