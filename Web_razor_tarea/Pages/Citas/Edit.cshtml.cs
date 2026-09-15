using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Citas
{
    public class EditModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public EditModel(VeterinariaContext context)
        {
            _context = context;
        }

        public SelectList MascotasLista    { get; set; } = default!;
        public SelectList VeterinariosLista { get; set; } = default!;

        [BindProperty]
        public Cita Cita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            Cita = cita;
            await CargarSelectoresAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarSelectoresAsync();
                return Page();
            }

            _context.Attach(Cita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Citas.Any(c => c.Id == Cita.Id))
                    return NotFound();
                throw;
            }

            TempData["Exito"] = "Cita actualizada correctamente.";
            return RedirectToPage("./Index");
        }

        private async Task CargarSelectoresAsync()
        {
            var mascotas = await _context.Mascotas
                .Where(m => m.Activo).OrderBy(m => m.Nombre).ToListAsync();
            var veterinarios = await _context.Veterinarios
                .Where(v => v.Activo).OrderBy(v => v.Apellidos).ToListAsync();

            MascotasLista     = new SelectList(mascotas,     "Id", "Nombre",    Cita.MascotaId);
            VeterinariosLista = new SelectList(veterinarios, "Id", "Apellidos", Cita.VeterinarioId);
        }
    }
}
