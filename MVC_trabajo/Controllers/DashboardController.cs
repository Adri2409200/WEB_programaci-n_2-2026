using Microsoft.AspNetCore.Mvc;

namespace MVC_trabajo.Controllers
{
    public class DashboardController : Controller
    {
        // Redirige al dashboard según el rol del usuario en sesión
        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");

            // Si no está logueado, va al login
            if (string.IsNullOrEmpty(rol))
                return RedirectToAction("Login", "Account");

            // Cada rol tiene su propia vista de dashboard
            return rol switch
            {
                "Administrador" => View("DashboardAdmin"),
                "Bibliotecario"  => View("DashboardBibliotecario"),
                _                => View("DashboardUsuario")
            };
        }
    }
}
