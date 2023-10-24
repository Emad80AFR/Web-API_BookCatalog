namespace FrameWork.Domain;

public class BaseClass<T>
{
    public BaseClass()
    {
        CreationDate = DateTime.Now;
    }

    public BaseClass(long id)
    {
        CreationDate = DateTime.Now;
        Id = Id;
    }

    public T Id { get; set; }
    public DateTime CreationDate { get; set; }
}