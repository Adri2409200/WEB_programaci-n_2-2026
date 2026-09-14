using System.ComponentModel.DataAnnotations;

namespace Web_razor_tarea.Models;

public class Veterinario
{
    public int Id { get; set; }

    // Nombre del veterinario
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    // Apellidos del veterinario
    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [StringLength(150)]
    [Display(Name = "Apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    // Área de especialidad
    [Required(ErrorMessage = "La especialidad es obligatoria")]
    [StringLength(120)]
    [Display(Name = "Especialidad")]
    public string Especialidad { get; set; } = string.Empty;

    // Teléfono de contacto
    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(20)]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    // true = activo, false = inactivo
    [Display(Name = "Estado")]
    public bool Activo { get; set; } = true;

    // Citas asignadas a este veterinario
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
