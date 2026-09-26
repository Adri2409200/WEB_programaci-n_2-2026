using System.ComponentModel.DataAnnotations;

namespace MVC_trabajo.Models
{
    // Modelo que registra cada préstamo de un libro a un usuario
    public class Prestamo
    {
        public int Id { get; set; }

        // Referencia al usuario que solicitó el préstamo
        [Required]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        // Referencia al libro prestado
        [Required]
        [Display(Name = "Libro")]
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

        // Fecha en que se realizó el préstamo
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de préstamo")]
        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        // Fecha límite para devolver el libro
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de devolución")]
        public DateTime FechaDevolucion { get; set; } = DateTime.Now.AddDays(7);

        // Estado: Pendiente, Devuelto, Vencido
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";
    }
}
