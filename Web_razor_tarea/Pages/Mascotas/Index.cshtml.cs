using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Mascotas
{
    public class IndexModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public IndexModel(VeterinariaContext context)
        {
            _context = context;
        }

        // Lista de mascotas con su propietario incluido
        public IList<Mascota> Mascotas { get; set; } = new List<Mascota>();

        public async Task OnGetAsync()
        {
            Mascotas = await _context.Mascotas
                .Include(m => m.Propietario)
                .OrderBy(m => m.Nombre)
                .ToListAsync();
        }
    }
}
