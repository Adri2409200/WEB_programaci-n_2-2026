using System.ComponentModel.DataAnnotations;

namespace Web_razor_tarea.Models;

// Estados posibles de una cita
public enum EstadoCita
{
    Pendiente,
    Completada,
    Cancelada
}

public class Cita
{
    public int Id { get; set; }

    // Mascota que asiste a la cita
    [Required(ErrorMessage = "La mascota es obligatoria")]
    [Display(Name = "Mascota")]
    public int MascotaId { get; set; }
    public Mascota? Mascota { get; set; }

    // Veterinario que atiende la cita
    [Required(ErrorMessage = "El veterinario es obligatorio")]
    [Display(Name = "Veterinario")]
    public int VeterinarioId { get; set; }
    public Veterinario? Veterinario { get; set; }

    // Fecha y hora de la cita
    [Required(ErrorMessage = "La fecha y hora son obligatorias")]
    [Display(Name = "Fecha y Hora")]
    public DateTime FechaHora { get; set; }

    // Razón por la que se agenda la cita
    [Required(ErrorMessage = "El motivo es obligatorio")]
    [StringLength(300)]
    [Display(Name = "Motivo")]
    public string Motivo { get; set; } = string.Empty;

    // Estado actual de la cita
    [Display(Name = "Estado")]
    public EstadoCita Estado { get; set; } = EstadoCita.Pendiente;

    // Resultado del diagnóstico (se llena después de la consulta)
    [StringLength(500)]
    [Display(Name = "Diagnóstico")]
    public string? Diagnostico { get; set; }
}
