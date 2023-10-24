using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;

public class CreateModel : PageModel
{
    private readonly IBookApplication _application;
    private readonly ILogger<IndexModel> _logger;

    public CreateModel(ILogger<IndexModel> logger, IBookApplication application)
    {
        _logger = logger;
        _application = application;
    }

    public CreateBook Command { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(CreateBook command, CancellationToken cancellationToken)
    {
        await _application.CreateBook(command, cancellationToken);
        return RedirectToPage("Index");
    }
}