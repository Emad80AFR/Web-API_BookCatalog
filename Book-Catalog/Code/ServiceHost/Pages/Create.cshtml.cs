using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CreateModel : PageModel
    {
        public CreateBook Command { get; set; }
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookApplication _application;
        public CreateModel(ILogger<IndexModel> logger, IBookApplication application)
        {
            _logger = logger;
            _application = application;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CreateBook command,CancellationToken cancellationToken)
        {
            await _application.CreateBook(command, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}