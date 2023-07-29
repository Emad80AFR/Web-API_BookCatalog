using BC.Domain.BookAgg;
using BC.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BC.Infrastructure.EFCore
{
    public class BookCatalogDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(BookMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}