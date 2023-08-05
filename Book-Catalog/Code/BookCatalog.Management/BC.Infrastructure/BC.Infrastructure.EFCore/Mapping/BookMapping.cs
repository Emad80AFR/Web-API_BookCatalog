using System.Security.Cryptography.X509Certificates;
using BC.Domain.BookAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BC.Infrastructure.EFCore.Mapping;

public class BookMapping:IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Author).HasMaxLength(256).IsRequired();


        //builder.HasData(
        //    new Book(-1,"شاهنامه", "فردوسی", new DateTime(1821, 6, 10)),
        //    new Book(-2,"لغت نامه دهخدا", "دهخدا", new DateTime(1920, 8, 24)));

    }
}