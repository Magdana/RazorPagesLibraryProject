using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.DTOes
{
    public class GenreCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
