using static System.Reflection.Metadata.BlobBuilder;

class Top5BooksToGet
{
    Dictionary<string, string> books = new Dictionary<string, string>()
        {
            { "1984", "George Orwell" },
            { "To Kill a Mockingbird", "Harper Lee" },
            { "Brave New World", "Aldous Huxley" },
            { "The Great Gatsby", "F. Scott Fitzgerald" },
            { "Moby Dick", "Herman Melville" }
        };

    public void PrintBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Key}, Author: {book.Value}");
        }
    }
}