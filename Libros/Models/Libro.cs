using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Libros.Models
{
    public class Libro
    {
        [Key]
        [Required(ErrorMessage = "El ISBN es obligatorio")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "El ISBN debe tener exactamente 13 dígitos numéricos")]
        public string ISBN { get; set; } = null!;

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; } = null!;

        [Required(ErrorMessage = "El autor es obligatorio")]
        public string Autor { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        public DateOnly FechaPublicacion { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        public string Genero { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ISBN != null && !ISBN.StartsWith("978"))
            {
                yield return new ValidationResult(
                    "El ISBN debe comenzar con 978",
                    new[] { nameof(ISBN) });
            }
        }
    }
}