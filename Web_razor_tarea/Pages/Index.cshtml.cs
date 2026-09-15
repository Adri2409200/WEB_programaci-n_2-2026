using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;

namespace Web_razor_tarea.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VeterinariaContext _context;

        public IndexModel(VeterinariaContext context)
        {
            _context = context;
        }

        // Contadores para las tarjetas del inicio
        public int TotalPropietarios { get; set; }
        public int TotalMascotas     { get; set; }
        public int TotalVeterinarios { get; set; }
        public int TotalCitas        { get; set; }
        public int CitasPendientes   { get; set; }

        public async Task OnGetAsync()
        {
            TotalPropietarios = await _context.Propietarios.CountAsync();
            TotalMascotas     = await _context.Mascotas.CountAsync();
            TotalVeterinarios = await _context.Veterinarios.CountAsync();
            TotalCitas        = await _context.Citas.CountAsync();
            CitasPendientes   = await _context.Citas
                .CountAsync(c => c.Estado == Models.EstadoCita.Pendiente);
        }
    }
}
