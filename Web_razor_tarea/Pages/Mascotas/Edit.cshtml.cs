using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Mascotas
{
    public class EditModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public EditModel(VeterinariaContext context)
        {
            _context = context;
        }

        public SelectList PropietariosLista { get; set; } = default!;

        [BindProperty]
        public Mascota Mascota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null) return NotFound();

            Mascota = mascota;
            await CargarPropietariosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarPropietariosAsync();
                return Page();
            }

            _context.Attach(Mascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Mascotas.Any(m => m.Id == Mascota.Id))
                    return NotFound();
                throw;
            }

            return RedirectToPage("./Index");
        }

        private async Task CargarPropietariosAsync()
        {
            var propietarios = await _context.Propietarios
                .Where(p => p.Activo)
                .OrderBy(p => p.Apellidos)
                .ToListAsync();

            PropietariosLista = new SelectList(
                propietarios, "Id", "Apellidos", Mascota.PropietarioId
            );
        }
    }
}
