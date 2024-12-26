using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.Entities
{
    public class BookEntity:BaseEntity
    {
        [Required]
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public int InitialCount { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        public int? GenreId { get; set; }
        public GenreEntity? Genre { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        public string? ImagePath { get; set; }

    }
}
