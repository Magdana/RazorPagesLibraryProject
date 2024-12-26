using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
