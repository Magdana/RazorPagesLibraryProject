using AutoMapper;
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
        private readonly IMapper _mapper;

        public GenrePageModel(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
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

        public async Task OnGet()
        {
            try
            {
                Genres = await _genreService.GetAllAsync();

                if (!string.IsNullOrEmpty(SearchWord))
                {
                    Genres = await _genreService.Search(SearchWord);
                }
            }
            catch (Exception ex)
            {
                Genres = new List<GenreGetDTO>();
            }
        }

        public async Task<IActionResult> OnPostAddGenreAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(NewGenreName))
                {
                    var genre = new GenreCreateDTO { Name = NewGenreName };
                    await _genreService.Add(genre);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteGenreAsync()
        {
            try
            {
                var genre = new GenreGetDTO { Id = GenreId };

                if (genre != null)
                {
                    await _genreService.Delete(genre);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditGenreAsync()
        {
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
                return Page();
            }
        }

    }

}
