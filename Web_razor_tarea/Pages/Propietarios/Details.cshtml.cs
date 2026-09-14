using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web_razor_tarea.Data;
using Web_razor_tarea.Models;

namespace Web_razor_tarea.Pages.Propietarios
{
    public class DetailsModel : PageModel
    {
        private readonly Web_razor_tarea.Data.VeterinariaContext _context;

        public DetailsModel(Web_razor_tarea.Data.VeterinariaContext context)
        {
            _context = context;
        }

        public Propietario Propietario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propietario = await _context.Propietarios.FirstOrDefaultAsync(m => m.Id == id);

            if (propietario is not null)
            {
                Propietario = propietario;

                return Page();
            }

            return NotFound();
        }
    }
}
