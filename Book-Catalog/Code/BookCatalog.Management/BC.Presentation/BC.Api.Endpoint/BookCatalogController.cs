using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BC.Api.Endpoint;

[ApiController]
[Route("api/[controller]")]
public class BookCatalogController : ControllerBase
{
    private readonly IBookApplication _application;


    private readonly IMemoryCache _cache;

    public BookCatalogController(IBookApplication application, IMemoryCache cache)
    {
        _cache = cache;
        _application = application;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        if (!_cache.TryGetValue("allBooks", out IEnumerable<BookViewModel> books))
        {
            books = await _application.GetAllBooks(cancellationToken);

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
            _cache.Set("allBooks", books, cacheEntryOptions);
        }

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<BookViewModel> GetByAsync(long id, CancellationToken cancellationToken)
    {
        return await _application.GetBook(id, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _application.DeleteBook(id, cancellationToken);
    }

    [HttpPost]
    public async Task CreateBook(CreateBook command, CancellationToken cancellationToken)
    {
        await _application.CreateBook(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task EditBook(int? id, EditBook command, CancellationToken cancellationToken)
    {
        await _application.EditBook(command, cancellationToken);
    }
}