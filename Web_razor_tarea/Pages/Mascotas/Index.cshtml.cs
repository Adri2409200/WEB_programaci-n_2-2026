using Microsoft.AspNetCore.Mvc;
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

        public IList<Mascota> Mascotas { get; set; } = new List<Mascota>();

        // Texto de búsqueda
        [BindProperty(SupportsGet = true)]
        public string? Busqueda { get; set; }

        public async Task OnGetAsync()
        {
            var consulta = _context.Mascotas
                .Include(m => m.Propietario)
                .AsQueryable();

            // Filtra por nombre, especie o apellido del propietario
            if (!string.IsNullOrWhiteSpace(Busqueda))
            {
                var termino = Busqueda.Trim().ToLower();
                consulta = consulta.Where(m =>
                    m.Nombre.ToLower().Contains(termino) ||
                    m.Especie.ToLower().Contains(termino) ||
                    m.Raza.ToLower().Contains(termino) ||
                    m.Propietario!.Apellidos.ToLower().Contains(termino));
            }

            Mascotas = await consulta.OrderBy(m => m.Nombre).ToListAsync();
        }
    }
}
