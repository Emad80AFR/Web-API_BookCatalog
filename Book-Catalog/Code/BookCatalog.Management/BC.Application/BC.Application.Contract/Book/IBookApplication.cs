using BC.Application.Contract.Book.DTO_s;

namespace BC.Application.Contract.Book;

public interface IBookApplication
{
    Task CreateBook(CreateBook command,CancellationToken cancellationToken);
    Task EditBook(EditBook command,CancellationToken cancellationToken);
    Task DeleteBook(long id, CancellationToken cancellationToken);
    Task<BookViewModel> GetBook(long id, CancellationToken cancellationToken);
    Task<EditBook> GetBookDetail(long id, CancellationToken cancellationToken);
    Task<List<BookViewModel>> GetAllBooksBy(BookSearchModel search,CancellationToken cancellationToken);
    Task<List<BookViewModel>> GetAllBooks(CancellationToken cancellationToken);


}