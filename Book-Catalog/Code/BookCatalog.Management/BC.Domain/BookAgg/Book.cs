using FrameWork.Domain;

namespace BC.Domain.BookAgg;

public class Book:BaseClass<long>
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public DateTime PublishYear { get; private set; }

    public Book(string title, string author,DateTime publishYear)
    {
        Title = title;
        Author = author;
        PublishYear = publishYear;
    }

    public void Edit(string title, string author,DateTime publishYear)
    {
        Title = title;
        Author = author;
        PublishYear = publishYear;
    }

}