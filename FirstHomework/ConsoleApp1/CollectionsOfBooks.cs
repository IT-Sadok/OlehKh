using static System.Reflection.Metadata.BlobBuilder;

public class CollectionsOfBooks : FileHandler
{
    public List<Book> NewBooks { get; private set; } = new List<Book>();
    public string _saveFilePath { get; private set; }

    public CollectionsOfBooks(List<Book> books, string _saveFilePath)
    {
        UpdateNewBooks(books);
    }

    private void UpdateNewBooks(List<Book> books)
    {
        NewBooks = books.Skip(Math.Max(0, books.Count - 5)).ToList();
    }
}