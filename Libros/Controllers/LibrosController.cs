using Libros.Data;
using Libros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LibrosController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<Libro>> GetLibro(string isbn)
        {
            var libro = await _context.Libros.FindAsync(isbn);
            if (libro == null) return NotFound();
            return libro;
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLibro), new { isbn = libro.ISBN }, libro);
        }

        [HttpPut("{isbn}")]
        public async Task<IActionResult> PutLibro(string isbn, Libro libro)
        {
            if (isbn != libro.ISBN)
                return BadRequest("El ISBN de la URL no coincide con el del cuerpo.");

            var libroExistente = await _context.Libros.FindAsync(isbn);
            if (libroExistente == null)
                return NotFound();

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.FechaPublicacion = libro.FechaPublicacion;
            libroExistente.Genero = libro.Genero;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteLibro(string isbn)
        {
            var libro = await _context.Libros.FindAsync(isbn);
            if (libro == null) return NotFound();

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("buscar/{busqueda}")]
        public async Task<ActionResult<IEnumerable<Libro>>> BuscarLibros(string busqueda)
        {
            var libros = await _context.Libros
                .Where(l => EF.Functions.Like(l.ISBN, $"%{busqueda}%") ||
                            EF.Functions.Like(l.Titulo, $"%{busqueda}%"))
                .OrderBy(l => l.Titulo)
                .ToListAsync();

            if (!libros.Any())
            {
                return NotFound("No se encontraron libros que coincidan con la búsqueda.");
            }

            return Ok(libros);
        }
    }
}
