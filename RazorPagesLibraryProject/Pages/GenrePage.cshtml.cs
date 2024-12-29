using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Services.Interfaces;
using RazorPagesLibraryProject.Services.Services;

namespace RazorPagesLibraryProject.Pages
{
    public class GenrePageModel : PageModel
    {
        private readonly IGenreService _genreService;

        public GenrePageModel(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public List<GenreGetDTO> Genres { get; set; }
        [BindProperty]
        public string NewGenreName { get; set; }
        [BindProperty]
        public int GenreId { get; set; }

        public async Task OnGet()
        {
            Genres = await _genreService.GetAllAsync();
        }
        public async Task<IActionResult> OnPostAddGenreAsync()
        {
            if (!string.IsNullOrEmpty(NewGenreName))
            {
                var genre = new GenreCreateDTO { Name = NewGenreName };
                await _genreService.Add(genre);
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteGenreAsync()
        {
            var genre = new GenreGetDTO { Id = GenreId };

            if (genre != null)
            {
                await _genreService.Delete(genre);
            }
            return RedirectToPage();
        }

    }
}
