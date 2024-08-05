public class BookFactory : IMediaFactory
{
    public Media CreateMedia(string title, string author)
    {
        return new Book(title, author);
    }
}
