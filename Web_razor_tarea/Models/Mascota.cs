using System.ComponentModel.DataAnnotations;

namespace Web_razor_tarea.Models;

public class Mascota
{
    public int Id { get; set; }

    // Propietario al que pertenece la mascota
    [Required(ErrorMessage = "El propietario es obligatorio")]
    [Display(Name = "Propietario")]
    public int PropietarioId { get; set; }
    public Propietario? Propietario { get; set; }

    // Nombre de la mascota
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    // Especie (perro, gato, etc.)
    [Required(ErrorMessage = "La especie es obligatoria")]
    [StringLength(80)]
    [Display(Name = "Especie")]
    public string Especie { get; set; } = string.Empty;

    // Raza de la mascota
    [StringLength(100)]
    [Display(Name = "Raza")]
    public string Raza { get; set; } = string.Empty;

    // Fecha de nacimiento
    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Nacimiento")]
    public DateOnly FechaNacimiento { get; set; }

    // true = activo, false = inactivo
    [Display(Name = "Estado")]
    public bool Activo { get; set; } = true;

    // Citas asociadas a esta mascota
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
