using System.Data.Entity;

namespace LinqKit_Issue59.Entities
{
    class TestDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
