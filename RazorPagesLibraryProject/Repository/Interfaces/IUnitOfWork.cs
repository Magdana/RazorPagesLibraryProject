namespace RazorPagesLibraryProject.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository bookRepository { get; }
        IGenreRepository genreRepository { get; }
    }
}
