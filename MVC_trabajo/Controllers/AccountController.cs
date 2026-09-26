using Microsoft.AspNetCore.Mvc;
using MVC_trabajo.Data;
using MVC_trabajo.Models;

namespace MVC_trabajo.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        // Muestra el formulario de login
        public IActionResult Login()
        {
            // Si ya está logueado, redirige al dashboard
            if (HttpContext.Session.GetString("UsuarioRol") != null)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        // Procesa el formulario de login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel datos)
        {
            if (!ModelState.IsValid)
                return View(datos);

            // Busca el usuario por correo y contraseña
            var usuario = _db.Usuarios.FirstOrDefault(u =>
                u.Correo == datos.Correo && u.Contrasena == datos.Contrasena);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(datos);
            }

            // Guarda los datos del usuario en la sesión
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);

            // Redirige al dashboard según el rol
            return RedirectToAction("Index", "Dashboard");
        }

        // Cierra la sesión del usuario
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
