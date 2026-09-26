using System.ComponentModel.DataAnnotations;

namespace MVC_trabajo.Models
{
    // Modelo que representa los libros disponibles en la biblioteca
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El autor es obligatorio")]
        [Display(Name = "Autor")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ISBN es obligatorio")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Display(Name = "Editorial")]
        public string? Editorial { get; set; }

        [Display(Name = "Año de publicación")]
        public int? Anio { get; set; }

        [Display(Name = "Categoría")]
        public string? Categoria { get; set; }

        // Cantidad total de ejemplares del libro
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0, 9999, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        [Display(Name = "Cantidad disponible")]
        public int Cantidad { get; set; } = 1;

        // Préstamos asociados a este libro
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
