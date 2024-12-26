namespace RazorPagesLibraryProject.Entities
{
    public class GenreEntity:BaseEntity
    {
        public List<BookEntity>? BooksList { get; set; }
    }
}
