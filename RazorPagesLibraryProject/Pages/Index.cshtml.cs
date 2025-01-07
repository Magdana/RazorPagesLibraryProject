using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task OnGet()
        {
            await CarouselAsync();
        }

        public async Task CarouselAsync()
        {
            Urls = await _bookService.GetLastAdded();
        }
    }
}
