using System.Globalization;
using BC.Application.Contract.Book.DTO_s;
using BC.Domain.BookAgg;
using FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BC.Infrastructure.EFCore.Repository;

public class BookRepository : BaseRepository<long, Book>, IBookRepository
{
    private readonly BookCatalogDbContext _bookCatalogDbContext;
    private readonly ILogger<BookRepository> _logger;

    public BookRepository(ILogger<BookRepository> logger, BookCatalogDbContext bookCatalogDbContext) : base(
        bookCatalogDbContext, logger)
    {
        _logger = logger;
        _bookCatalogDbContext = bookCatalogDbContext;
    }

    public async Task<BookViewModel?> GetByBookId(long id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching data from database by id:{id}", id);
            var book = await _bookCatalogDbContext.Books.Select(x => new BookViewModel
            {
                Author = x.Author,
                Id = x.Id,
                PublishYear = x.PublishYear,
                Title = x.Title
            }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (book != null)
                _logger.LogInformation("Book retrieved successfully. Book ID: {BookId}", book.Id);
            else
                _logger.LogWarning("No Book found with ID: {BookId}", id);
            return book;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching data");
            throw;
        }
    }

    public async Task<EditBook?> GetBookDetail(long id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching data from database by id:{id}", id);
            var book = await _bookCatalogDbContext.Books.Select(x => new EditBook
            {
                Author = x.Author,
                Id = x.Id,
                PublishDate = x.PublishYear.ToString(CultureInfo.InvariantCulture),
                Title = x.Title
            }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (book != null)
                _logger.LogInformation("Book retrieved successfully. Book ID: {BookId}", book.Id);
            else
                _logger.LogWarning("No Book found with ID: {BookId}", id);
            return book;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching data");
            throw;
        }
    }

    public async Task<List<BookViewModel>> GetAllBooks(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching data from database ");
            var books = await _bookCatalogDbContext.Books.Select(x => new BookViewModel
            {
                Author = x.Author,
                Id = x.Id,
                PublishYear = x.PublishYear,
                Title = x.Title
            }).ToListAsync(cancellationToken);
            if (books != null)
                _logger.LogInformation("Books retrieved successfully.");
            else
                _logger.LogWarning("Books retrieved unsuccessfully.");
            return books!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching data");
            throw;
        }
    }

    public async Task DeleteBook(long id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching data from database by id:{id} for deleting ", id);
            var book = await _bookCatalogDbContext.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (book != null)
            {
                _logger.LogInformation("Book retrieved successfully. Book ID: {BookId} ", book.Id);
                _bookCatalogDbContext.Books.Remove(book);
                _logger.LogInformation("Book deleted successfully. Book ID: {BookId} ", book.Id);
            }

            else
            {
                _logger.LogWarning("No Book found with ID: {BookId} for deleting", id);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching data");
            throw;
        }
    }

    public async Task<List<BookViewModel>> GetAllBooksBy(BookSearchModel searchModel,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching data from database ");
            var books = _bookCatalogDbContext.Books.Select(x => new BookViewModel
            {
                Author = x.Author,
                Id = x.Id,
                PublishYear = x.PublishYear,
                Title = x.Title
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                books = books.Where(x => x.Title.Contains(searchModel.Title));

            if (books != null)
                _logger.LogInformation("Books retrieved successfully.");
            else
                _logger.LogWarning("Books retrieved unsuccessfully.");
            return await books!.ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while fetching data");
            throw;
        }
    }
}