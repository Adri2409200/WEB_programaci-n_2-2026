using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Veterinarios
{
    public class IndexModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public IndexModel(VeterinariaContext context)
        {
            _context = context;
        }

        // Lista de veterinarios ordenada por apellido
        public IList<Veterinario> Veterinarios { get; set; } = new List<Veterinario>();

        public async Task OnGetAsync()
        {
            Veterinarios = await _context.Veterinarios
                .OrderBy(v => v.Apellidos)
                .ToListAsync();
        }
    }
}
