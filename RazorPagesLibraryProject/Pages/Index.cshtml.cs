using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.DTOes;
using RazorPagesLibraryProject.Services.Interfaces;

namespace RazorPagesLibraryProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookService _bookService;

        public IndexModel(ILogger<IndexModel> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public List<BookGetDTO>? Books { get; set; }
        public List<string>? Urls { get; set; } = new List<string>();
        public string? RandomBook { get; set; }
        public string? ImageUrl { get; set; }
        public int Id {  get; set; }

        public async Task OnGet()
        {
            await CarouselAsync();
            var books = await _bookService.GetAllAsync();
            if (books.Count == 0)
            {
                RandomBook = "No books available.";
            }
            else
            {
                Random random = new Random();
                var randBook = books[random.Next(books.Count)];
                RandomBook = randBook.ImagePath;
                Id = randBook.Id;
            }

            Page();
        }

        public async Task CarouselAsync()
        {
            Urls = await _bookService.GetLastAdded();
        }
    }
}
