using BC.Domain.BookAgg;
using BC.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BC.Infrastructure.EFCore;

public class BookCatalogDbContext : DbContext
{
    public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(BookMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        SeedData.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "شاهنامه", Author = "فردوسی", PublishYear = new DateTime(1821, 6, 10) },
                new Book { Id = 2, Title = "لغت نامه دهخدا", Author = "دهخدا", PublishYear = new DateTime(1920, 8, 24) }
            );
        }
    }
}