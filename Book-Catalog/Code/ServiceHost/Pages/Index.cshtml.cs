using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IBookApplication _application;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IBookApplication application)
    {
        _logger = logger;
        _application = application;
    }

    public List<BookViewModel> Books { get; set; }
    public BookSearchModel Search { get; set; }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        Books = await _application.GetAllBooks(cancellationToken);
    }

    public async Task OnPostSearch(BookSearchModel search, CancellationToken cancellationToken)
    {
        Books = await _application.GetAllBooksBy(search, cancellationToken);
    }

    public async Task<IActionResult> OnGetDelete(long id, CancellationToken cancellationToken)
    {
        await _application.DeleteBook(id, cancellationToken);
        return RedirectToPage("/Index");
    }
}