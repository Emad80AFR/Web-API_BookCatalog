using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EditModel : PageModel
    {
        public EditBook Book { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookApplication _application;
        public EditModel(ILogger<IndexModel> logger, IBookApplication application)
        {
            _logger = logger;
            _application = application;
        }

        public async Task  OnGet(long id,CancellationToken cancellationToken)
        {
           Book = await _application.GetBookDetail(id, cancellationToken);
        }

        public async Task<IActionResult> OnPost(EditBook book,CancellationToken cancellationToken)
        {
            await _application.EditBook(book, cancellationToken);
            return RedirectToPage("/Index");  
        }
    }
}