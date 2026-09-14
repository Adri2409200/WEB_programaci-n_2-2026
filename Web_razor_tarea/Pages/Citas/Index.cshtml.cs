using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Citas
{
    public class IndexModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public IndexModel(VeterinariaContext context)
        {
            _context = context;
        }

        // Lista de citas con mascota y veterinario incluidos
        public IList<Cita> Citas { get; set; } = new List<Cita>();

        public async Task OnGetAsync()
        {
            Citas = await _context.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Veterinario)
                .OrderByDescending(c => c.FechaHora)
                .ToListAsync();
        }
    }
}
