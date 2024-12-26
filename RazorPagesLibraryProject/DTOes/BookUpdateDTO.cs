using RazorPagesLibraryProject.Entities;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.DTOes
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        [Required]
        public string Author { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public int InitialCount { get; set; }
        public int? GenreId { get; set; }
        public GenreEntity? Genre { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
