using System.ComponentModel.DataAnnotations;

namespace Libros.Models
{
    public class Libro
    {
        [Key]
        public String ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateOnly FechaPublicacion { get; set; }
        public string Genero { get; set; }

    }
}
