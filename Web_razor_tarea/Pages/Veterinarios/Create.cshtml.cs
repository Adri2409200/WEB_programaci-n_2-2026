using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Veterinarios
{
    public class CreateModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public CreateModel(VeterinariaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Veterinario Veterinario { get; set; } = default!;

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Veterinarios.Add(Veterinario);
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Veterinario '{Veterinario.Nombre} {Veterinario.Apellidos}' registrado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}
