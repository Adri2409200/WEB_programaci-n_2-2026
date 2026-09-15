using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Propietarios
{
    public class CreateModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public CreateModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Propietario Propietario { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Propietarios.Add(Propietario);
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Propietario '{Propietario.Nombre} {Propietario.Apellidos}' registrado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}
