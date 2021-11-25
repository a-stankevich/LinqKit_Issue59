using System;
using System.Linq;
using LinqKit;
using LinqKit_Issue59.Entities;

namespace LinqKit_Issue59
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new TestDbContext();
            // trigger context initialization
            context.Books.ToList();

            // now start logging DB requests
            context.Database.Log = Console.WriteLine;

            Console.WriteLine("This produces one query:");

            RunQuery(false);

            Console.WriteLine("This produces two queries:");

            RunQuery(true);

            void RunQuery(bool useAsExpandable)
            {
                IQueryable<Book> booksQuery = context.Books;
                if (useAsExpandable)
                {
                    booksQuery = booksQuery.AsExpandable();
                }
                var authorsQuery =
                    from a in context.Authors
                    select new
                    {
                        a.FullName,
                        Count = booksQuery.Where(b => b.Author.Id == a.Id)
                    };
                authorsQuery.ToList();
            }
        }
    }
}
