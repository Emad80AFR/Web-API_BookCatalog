using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        public List<BookViewModel> Books { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookApplication _application;
        public IndexModel(ILogger<IndexModel> logger, IBookApplication application)
        {
            _logger = logger;
            _application = application;
        }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Books = await _application.GetAllBooks(cancellationToken);
        }
        public async Task<IActionResult> OnGetDelete(long id, CancellationToken cancellationToken) 
        {
            await _application.DeleteBook(id, cancellationToken);
            return RedirectToPage("/Index");
        }
    }
}