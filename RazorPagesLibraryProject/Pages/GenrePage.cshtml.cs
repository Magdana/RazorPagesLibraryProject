using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Services.Interfaces;
using RazorPagesLibraryProject.Services.Services;
using System.Linq;

namespace RazorPagesLibraryProject.Pages
{
    public class GenrePageModel : PageModel
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<GenrePageModel> _logger;

        public GenrePageModel(IGenreService genreService, IMapper mapper, IBookService bookService, ILogger<GenrePageModel> logger)
        {
            _genreService = genreService;
            _mapper = mapper;
            _bookService = bookService;
            _logger = logger;
        }
        public List<GenreGetDTO> Genres { get; set; }
        [BindProperty]
        public string NewGenreName { get; set; }
        [BindProperty]
        public int GenreId { get; set; }
        [BindProperty]
        public string UpdatedGenreName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchWord { get; set; }
        [BindProperty]
        public int Count { get; set; }

        public async Task OnGet()
        {
            try
            {
                var genres = await _genreService.GetAllAsync();

                foreach (var genre in genres)
                {
                    var books = await _bookService.GetBooksByGenresAsync(genre.Id);
                    genre.Count = books.Count();
                }

                Genres = genres;

                if (!string.IsNullOrEmpty(SearchWord))
                {
                    Genres = await _genreService.Search(SearchWord);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all genres");
                Genres = new List<GenreGetDTO>();
            }
        }

        public async Task<IActionResult> OnPostAddGenreAsync()
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Forbid();
                }
                if (!string.IsNullOrEmpty(NewGenreName))
                {
                    var genre = new GenreCreateDTO { Name = NewGenreName };
                    await _genreService.Add(genre);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new genre.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteGenreAsync()
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Forbid();
                }
                var genre = new GenreGetDTO { Id = GenreId };

                if (genre != null)
                {
                    await _genreService.Delete(genre);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a genre.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditGenreAsync()
        {
            if (!User.IsInRole("admin"))
            {
                return Forbid();
            }
            if (string.IsNullOrEmpty(UpdatedGenreName))
            {
                return RedirectToPage();
            }

            try
            {
                var genre = await _genreService.GetById(GenreId);
                if (genre == null)
                {
                    ModelState.AddModelError(string.Empty, "Genre not found.");
                    return Page();
                }

                genre.Name = UpdatedGenreName;
                var mappedtoEntity = _mapper.Map<GenreEntity>(genre);
                var mappedtoupdatemodel = _mapper.Map<GenreUpdateDTO>(mappedtoEntity);
                await _genreService.Update(mappedtoupdatemodel);

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while editing a genre.");
                return Page();
            }
        }

    }

}
