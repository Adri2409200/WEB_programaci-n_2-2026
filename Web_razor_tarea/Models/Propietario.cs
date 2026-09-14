using System.ComponentModel.DataAnnotations;

namespace Web_razor_tarea.Models;

public class Propietario
{
    public int Id { get; set; }

    // Nombre del propietario
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    // Apellidos del propietario
    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [StringLength(150)]
    [Display(Name = "Apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    // Teléfono de contacto
    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(20)]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    // Correo electrónico
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(200)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    // true = activo, false = inactivo
    [Display(Name = "Estado")]
    public bool Activo { get; set; } = true;

    // Mascotas que pertenecen a este propietario
    public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
}
