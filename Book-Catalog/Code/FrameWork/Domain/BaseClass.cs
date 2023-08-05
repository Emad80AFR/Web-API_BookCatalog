﻿namespace FrameWork.Domain;

public class BaseClass<T>
{
    public T Id { get; set; }
    public DateTime CreationDate { get; set; }

    public BaseClass()
    {
        CreationDate = DateTime.Now;
    }
    public BaseClass(long id)
    {
        CreationDate = DateTime.Now;
        Id = Id;
    }
}