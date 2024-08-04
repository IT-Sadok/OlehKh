public class Book: Media
{
    public string Author { get; set; }

    public Book() { }

    public Book(string title, string author) : base(title)
    {
        Author = author;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }
}