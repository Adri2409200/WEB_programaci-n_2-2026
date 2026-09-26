using System.ComponentModel.DataAnnotations;

namespace MVC_trabajo.Models
{
    // Modelo que representa a los usuarios del sistema
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; } = string.Empty;

        // Rol: Administrador, Bibliotecario o Usuario
        [Required(ErrorMessage = "El rol es obligatorio")]
        [Display(Name = "Rol")]
        public string Rol { get; set; } = "Usuario";

        // Lista de préstamos que tiene este usuario
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
