using Microsoft.AspNetCore.Mvc;
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

        public IList<Cita> Citas { get; set; } = new List<Cita>();

        // Texto de búsqueda
        [BindProperty(SupportsGet = true)]
        public string? Busqueda { get; set; }

        // Filtro por estado
        [BindProperty(SupportsGet = true)]
        public string? FiltroEstado { get; set; }

        public async Task OnGetAsync()
        {
            var consulta = _context.Citas
                .Include(c => c.Mascota)
                .Include(c => c.Veterinario)
                .AsQueryable();

            // Filtra por nombre de mascota o apellido del veterinario
            if (!string.IsNullOrWhiteSpace(Busqueda))
            {
                var termino = Busqueda.Trim().ToLower();
                consulta = consulta.Where(c =>
                    c.Mascota!.Nombre.ToLower().Contains(termino) ||
                    c.Veterinario!.Apellidos.ToLower().Contains(termino) ||
                    c.Motivo.ToLower().Contains(termino));
            }

            // Filtra por estado si se seleccionó uno
            if (!string.IsNullOrWhiteSpace(FiltroEstado) &&
                Enum.TryParse<EstadoCita>(FiltroEstado, out var estado))
            {
                consulta = consulta.Where(c => c.Estado == estado);
            }

            Citas = await consulta.OrderByDescending(c => c.FechaHora).ToListAsync();
        }
    }
}
