using RazorPagesLibraryProject.DataAccess;
using RazorPagesLibraryProject.Repository.Interfaces;

namespace RazorPagesLibraryProject.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepository bookRepository { get; private set; }
        public IGenreRepository genreRepository { get; private set; }

        private readonly LibraryDbContext _context;
        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            bookRepository = new BookRepository(context);
            genreRepository = new GenreRepository(context);
        }

    }
}
