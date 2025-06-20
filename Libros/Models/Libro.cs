using System.ComponentModel.DataAnnotations;

namespace Libros.Models
{
    public class Libro
    {
        [Key]
        [Required]
        public String ISBN { get; set; } = null!;
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        public string Autor { get; set; } = null!;
        [Required]
        public DateOnly FechaPublicacion { get; set; } 
        [Required]
        public string Genero { get; set; } = null!;

    }
}
