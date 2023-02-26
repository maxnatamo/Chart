using Chart.Core.Parser.Execution;

namespace Test
{
    public class Author
    {
        public string Id { get; set; } = "default";
    }

    public class Book
    {
        public Author? Author { get; set; } = new Author();
        
        public List<Author> Ids { get; set; } = new List<Author>();

        public int Test(List<int>? id)
        {
            return id?.Count ?? 123;
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            Document doc = new Document()
                .RegisterType<Author>()
                .RegisterType<Book>();

            doc.Execute(_ =>
            {
                _.Query = "{ book }";
                _.Data = new { book = 1 };
            });

            Console.WriteLine(doc.GetSchema());
        }
    }
}
