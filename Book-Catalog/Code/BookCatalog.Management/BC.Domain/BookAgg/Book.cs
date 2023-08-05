using FrameWork.Domain;

namespace BC.Domain.BookAgg;

public class Book:BaseClass<long>
{
    public string Title { get;  set; }
    public string Author { get;  set; }
    public DateTime PublishYear { get;  set; }

    public Book() { }
    public Book(string title, string author,DateTime publishYear)
    {
        Title = title;
        Author = author;
        PublishYear = publishYear;
    }
    public Book(long id, string title, string author, DateTime publishYear) : base(id)
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