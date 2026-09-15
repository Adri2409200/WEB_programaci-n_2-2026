using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Citas
{
    public class CreateModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public CreateModel(VeterinariaContext context)
        {
            _context = context;
        }

        public SelectList MascotasLista    { get; set; } = default!;
        public SelectList VeterinariosLista { get; set; } = default!;

        [BindProperty]
        public Cita Cita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
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

            _context.Citas.Add(Cita);
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Cita registrada correctamente.";
            return RedirectToPage("./Index");
        }

        private async Task CargarSelectoresAsync()
        {
            var mascotas = await _context.Mascotas
                .Where(m => m.Activo).OrderBy(m => m.Nombre).ToListAsync();
            var veterinarios = await _context.Veterinarios
                .Where(v => v.Activo).OrderBy(v => v.Apellidos).ToListAsync();

            MascotasLista     = new SelectList(mascotas,     "Id", "Nombre");
            VeterinariosLista = new SelectList(
                veterinarios.Select(v => new { v.Id, NombreCompleto = $"{v.Nombre} {v.Apellidos}" }),
                "Id", "NombreCompleto");
        }
    }
}
