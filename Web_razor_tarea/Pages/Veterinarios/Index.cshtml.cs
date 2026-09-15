using Microsoft.AspNetCore.Mvc;
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

        public IList<Veterinario> Veterinarios { get; set; } = new List<Veterinario>();

        // Texto de búsqueda
        [BindProperty(SupportsGet = true)]
        public string? Busqueda { get; set; }

        public async Task OnGetAsync()
        {
            var consulta = _context.Veterinarios.AsQueryable();

            // Filtra por nombre, apellidos o especialidad
            if (!string.IsNullOrWhiteSpace(Busqueda))
            {
                var termino = Busqueda.Trim().ToLower();
                consulta = consulta.Where(v =>
                    v.Nombre.ToLower().Contains(termino) ||
                    v.Apellidos.ToLower().Contains(termino) ||
                    v.Especialidad.ToLower().Contains(termino));
            }

            Veterinarios = await consulta.OrderBy(v => v.Apellidos).ToListAsync();
        }
    }
}
