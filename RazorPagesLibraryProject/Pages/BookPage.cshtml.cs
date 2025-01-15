using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Services.Interfaces;
using RazorPagesLibraryProject.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesLibraryProject.Pages
{
    public class BookPageModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookPageModel> _logger;
        public BookPageModel(IBookService bookService, IMapper mapper, IGenreService genreService, ILogger<BookPageModel> logger)
        {
            _bookService = bookService;
            _mapper = mapper;
            _genreService = genreService;
            _logger = logger;
        }

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; } = string.Empty;
        [BindProperty]
        public DateTime? CreatedAt { get; set; }
        [BindProperty]
        public string Author { get; set; } = string.Empty;
        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public int InitialCount { get; set; }
        [BindProperty]
        public bool IsAvailable { get; set; } = true;
        [BindProperty]
        public int? GenreId { get; set; }
        [BindProperty]
        public GenreEntity? Genre { get; set; }
        [BindProperty]
        public DateTime IssueDate { get; set; }
        [BindProperty]
        public IFormFile? NewBookImagePath { get; set; }
        [BindProperty]
        public string? CurrentImagePath { get; set; }

        public List<SelectListItem> Genres { get; set; } = new();

        public async Task<IActionResult> OnGet(int id)
        {

            var book = await _bookService.GetById(id);
            var genreEntities = await _genreService.GetAllAsync();
            Genres = genreEntities
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToList();

            if (book == null)
            {
                return NotFound();
            }

            Id = book.Id;
            Name = book.Name;
            Description = book.Description;
            InitialCount = book.InitialCount;
            GenreId = book.GenreId;
            IssueDate = book.IssueDate;
            CurrentImagePath = book.ImagePath;
            CreatedAt = book.CreatedAt;
            Author = book.Author;
            IsAvailable = book.IsAvailable;
            Genre = book.Genre;

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteBookAsync(int id)
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Forbid();
                }
                var book = new BookGetDTO { Id = id };

                if (book != null)
                {
                    await _bookService.Delete(book);
                }
                return RedirectToPage("AllBooksPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a book.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditBookAsync()
        {
            try
            {
                if (!User.IsInRole("admin"))
                {
                    return Forbid();
                }
                var book = await _bookService.GetById(Id);
                if (book == null)
                {
                    ModelState.AddModelError(string.Empty, "Book not found.");
                    return Page();
                }

                var genreEntities = await _genreService.GetAllAsync();
                Genres = genreEntities
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    })
                    .ToList();

                
                if (NewBookImagePath == null || NewBookImagePath.Length == 0)
                {
                    book.ImagePath = book.ImagePath ?? book.ImagePath;  
                }
                else
                {
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

                    book.ImagePath = $"/UploadedFiles/images/{fileName}"; 
                }

                if (GenreId == null)
                {
                    book.GenreId = book.GenreId ?? book.GenreId; 
                }
                else
                {
                    book.GenreId = GenreId.Value;
                }

                book.Name = Name;
                book.Author = Author;
                book.Description = Description;
                book.IssueDate = IssueDate;
                book.InitialCount = InitialCount;
                book.IsAvailable = IsAvailable;

                var updateDto = _mapper.Map<BookUpdateDTO>(book);
                updateDto.Id = Id;

                await _bookService.Update(updateDto);
                return RedirectToPage("BookPage", new { Id = Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the book.");
                _logger.LogError(ex, "Error occurred while editing a book.");
                return Page();
            }
        }

    }
}
