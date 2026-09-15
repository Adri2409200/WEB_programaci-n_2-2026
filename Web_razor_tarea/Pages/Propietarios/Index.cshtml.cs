using Microsoft.AspNetCore.Mvc;
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

        public IList<Propietario> Propietarios { get; set; } = new List<Propietario>();

        // Texto de búsqueda ingresado por el usuario
        [BindProperty(SupportsGet = true)]
        public string? Busqueda { get; set; }

        public async Task OnGetAsync()
        {
            var consulta = _context.Propietarios.AsQueryable();

            // Filtra por nombre, apellidos o email si hay texto de búsqueda
            if (!string.IsNullOrWhiteSpace(Busqueda))
            {
                var termino = Busqueda.Trim().ToLower();
                consulta = consulta.Where(p =>
                    p.Nombre.ToLower().Contains(termino) ||
                    p.Apellidos.ToLower().Contains(termino) ||
                    p.Email.ToLower().Contains(termino));
            }

            Propietarios = await consulta.OrderBy(p => p.Apellidos).ToListAsync();
        }
    }
}
