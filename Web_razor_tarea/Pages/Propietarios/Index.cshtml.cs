using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Propietarios
{
    public class IndexModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public IndexModel(VeterinariaContext context)
        {
            _context = context;
        }

        // Lista de propietarios que se muestra en la tabla
        public IList<Propietario> Propietarios { get; set; } = new List<Propietario>();

        public async Task OnGetAsync()
        {
            Propietarios = await _context.Propietarios
                .OrderBy(p => p.Apellidos)
                .ToListAsync();
        }
    }
}
