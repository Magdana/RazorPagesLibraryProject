using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.DTOes
{
    public class GenreUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
