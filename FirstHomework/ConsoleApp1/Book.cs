class Book
{
    // Properties specific to a Book
    public string Title { get; set; }
    public string Author { get; set; }

    // Constructor to initialize Book properties
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    // DisplayBookInfo
    public void DisplayBookInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}");
    }


}