using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Mascotas
{
    public class CreateModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public CreateModel(VeterinariaContext context)
        {
            _context = context;
        }

        public SelectList PropietariosLista { get; set; } = default!;

        [BindProperty]
        public Mascota Mascota { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
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

            _context.Mascotas.Add(Mascota);
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Mascota '{Mascota.Nombre}' registrada correctamente.";
            return RedirectToPage("./Index");
        }

        private async Task CargarPropietariosAsync()
        {
            var propietarios = await _context.Propietarios
                .Where(p => p.Activo)
                .OrderBy(p => p.Apellidos)
                .ToListAsync();

            // Muestra nombre completo en el selector
            PropietariosLista = new SelectList(
                propietarios.Select(p => new { p.Id, NombreCompleto = $"{p.Nombre} {p.Apellidos}" }),
                "Id", "NombreCompleto");
        }
    }
}
