using BC.Application.Contract.Book.DTO_s;
using FrameWork.Domain;

namespace BC.Domain.BookAgg;

public interface IBookRepository:IBaseRepository<long,Book>
{
    Task<BookViewModel?> GetByBookId(long id,CancellationToken cancellationToken);
    Task<EditBook?> GetBookDetail(long id,CancellationToken cancellationToken);
    Task<List<BookViewModel>> GetAllBooks(CancellationToken cancellationToken);
    Task DeleteBook(long id,CancellationToken cancellationToken);
}