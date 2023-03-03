namespace Chart.Core.TypeResolver.Tests
{
    public class Book
    {
        public string Name { get; set; } = "The Raven";

        public Author Author { get; set; } = new Author();
    }

    public class Author
    {
        public string Name { get; set; } = "Edgar Allan Poe";
    }
}