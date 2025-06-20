using Microsoft.EntityFrameworkCore;
using Libros.Models;

namespace Libros.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Libro> Libros { get; set; }
    }
}
