using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_trabajo.Filters
{
    // Filtro que verifica si el usuario tiene sesión activa
    // Si no está logueado, lo manda al login
    public class SoloLogueadoAttribute : ActionFilterAttribute
    {
        private readonly string[] _rolesPermitidos;

        // Se puede usar sin roles (solo verifica que esté logueado)
        // o pasando roles específicos: [SoloLogueado("Administrador", "Bibliotecario")]
        public SoloLogueadoAttribute(params string[] roles)
        {
            _rolesPermitidos = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext contexto)
        {
            var rol = contexto.HttpContext.Session.GetString("UsuarioRol");

            // Si no hay sesión, redirige al login
            if (string.IsNullOrEmpty(rol))
            {
                contexto.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // Si se especificaron roles y el usuario no tiene ninguno de esos roles
            if (_rolesPermitidos.Length > 0 && !_rolesPermitidos.Contains(rol))
            {
                // No tiene permiso, lo manda a su dashboard
                contexto.Result = new RedirectToActionResult("Index", "Dashboard", null);
                return;
            }

            base.OnActionExecuting(contexto);
        }
    }
}
