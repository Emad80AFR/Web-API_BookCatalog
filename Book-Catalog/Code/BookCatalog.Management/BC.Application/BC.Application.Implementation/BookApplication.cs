using BC.Application.Contract.Book;
using BC.Application.Contract.Book.DTO_s;
using BC.Domain.BookAgg;
using Microsoft.Extensions.Logging;

namespace BC.Application.Implementation
{
    public class  BookApplication : IBookApplication
    {
        private readonly ILogger<BookApplication> _logger;
        private readonly IBookRepository _repository;

        public BookApplication(ILogger<BookApplication> logger, IBookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task CreateBook(CreateBook command, CancellationToken cancellationToken)
        {
            var isSuccessful = DateTime.TryParse(command.PublishDate,out var publishDate);
            if (isSuccessful)
            {
                var book = new Book(command.Title, command.Author, publishDate);
                await _repository.Create(book, cancellationToken);
                await _repository.SaveChanges(cancellationToken);
            }
            else
            {
                throw new NotImplementedException();
            }

            
        }

        public async Task EditBook(EditBook command, CancellationToken cancellationToken)
        {
            var book = await _repository.Get(command.Id, cancellationToken);
            if (book == null)
            {
                throw new NotImplementedException();
            }
            var isSuccessful = DateTime.TryParse(command.PublishDate, out var publishDate);
            if (isSuccessful)
            {
                book.Edit(command.Title,command.Author,publishDate);
                await _repository.SaveChanges(cancellationToken);
            }

        }

        public async Task DeleteBook(long id, CancellationToken cancellationToken)
        {
            await _repository.DeleteBook(id, cancellationToken);
            await _repository.SaveChanges(cancellationToken);
        }

        public async Task<BookViewModel> GetBook(long id, CancellationToken cancellationToken)
        {
            return (await _repository.GetByBookId(id, cancellationToken))!;
        }

        public async Task<EditBook> GetBookDetail(long id, CancellationToken cancellationToken)
        {
           return (await _repository.GetBookDetail(id, cancellationToken))!;
        }

        public async Task<List<BookViewModel>> GetAllBooks(CancellationToken cancellationToken)
        {
            return await _repository.GetAllBooks(cancellationToken);
        }
    }
}