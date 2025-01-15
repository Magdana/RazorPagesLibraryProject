using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Services.Interfaces;
using System.Linq;

namespace RazorPagesLibraryProject.Pages
{
    public class AllBooksPageModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AllBooksPageModel> _logger;

        public AllBooksPageModel(IBookService bookService, IGenreService genreService, IWebHostEnvironment webHostEnvironment, ILogger<AllBooksPageModel> logger)
        {
            _bookService = bookService;
            _genreService = genreService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public List<BookGetDTO> Books { get; set; } = new();
        public List<SelectListItem> Genres { get; set; } = new();

        [TempData]
        public string? Message { get; set; }

        [BindProperty]
        public string NewBookName { get; set; }
        [BindProperty]
        public string NewBookAuthor { get; set; }
        [BindProperty]
        public string? NewBookDescription { get; set; }
        [BindProperty]
        public int NewBookInitialCount { get; set; }
        [BindProperty]
        public int? NewBookGenreId { get; set; }
        [BindProperty]
        public DateTime NewBookIssueDate { get; set; }
        [BindProperty]
        public IFormFile? NewBookImagePath { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchWord { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task OnGet()
        {
            try
            {
                Books = await _bookService.GetAllAsync();
                var genreEntities = await _genreService.GetAllAsync();
                Genres = genreEntities
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    })
                    .ToList();
                if (!string.IsNullOrEmpty(SearchWord))
                {
                    Books = await _bookService.Search(SearchWord);
                }
            }
            catch (Exception ex)
            {
                Message = "Error loading books or genres.";
                Books = new List<BookGetDTO>();
                Genres = new List<SelectListItem>();
                _logger.LogError(ex, "Error occurred while getting all books.");

            }
        }

        public async Task OnGetFilteredByGenre(int id)
        {
            try
            {
                id = Id;
                var books = await _bookService.GetBooksByGenresAsync(id);
                Books = books.ToList();
                var genreEntities = await _genreService.GetAllAsync();
                Genres = genreEntities
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    })
                    .ToList();
            }
            catch(Exception ex)
            {
                Message = "Error loading books or genres.";
                Books = new List<BookGetDTO>();
                Genres = new List<SelectListItem>();
                _logger.LogError(ex, "Error occurred while getting books  by genre.");
            }
        }

        public async Task<IActionResult> OnPostAddBookAsync()
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Forbid();
                }
                if (!ModelState.IsValid)
                {
                    var genreEntities = await _genreService.GetAllAsync();
                    Genres = genreEntities
                        .Select(g => new SelectListItem
                        {
                            Value = g.Id.ToString(),
                            Text = g.Name
                        })
                        .ToList();

                    Message = "Validation failed. Please correct the form.";
                    return Page();
                }
                if (NewBookImagePath == null || NewBookImagePath.Length == 0)
                {
                    ModelState.AddModelError("NewBookImagePath", "Please upload an image.");
                    return Page();
                }
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".svg" };
                var extension = Path.GetExtension(NewBookImagePath.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("NewBookImagePath", "Unsupported file type.");
                    return Page();
                }
                string fileName = $"{DateTime.Now.Ticks}{extension}";
                var fileUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles", "images");

                if (!Directory.Exists(fileUploadPath))
                {
                    Directory.CreateDirectory(fileUploadPath);
                }

                var exactPath = Path.Combine(fileUploadPath, fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await NewBookImagePath.CopyToAsync(stream);
                }
                var imagePath = $"/UploadedFiles/images/{fileName}";

                var book = new BookCreateDTO
                {
                    Name = NewBookName,
                    Author = NewBookAuthor,
                    Description = NewBookDescription,
                    GenreId = NewBookGenreId,
                    ImagePath = imagePath,
                    InitialCount = NewBookInitialCount,
                    IssueDate = NewBookIssueDate
                };
                await _bookService.Add(book);

                Message = "Book added successfully!";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Message = "An error occurred while adding the book.";
                _logger.LogError(ex, "Error occurred while adding a new book.");
                return Page();
            }
        }



    }
}
