using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.Entities;
using System.Collections.Generic;

namespace RazorPagesLibraryProject.DataAccess
{
    public class LibraryDbContext: DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        public DbSet<BookEntity>? Books { get; set; }
        public DbSet<GenreEntity>? Genres { get; set; }
    }
}
