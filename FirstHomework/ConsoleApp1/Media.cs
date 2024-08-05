public abstract class Media
{
    public string Title { get; set; }

    public string title
    {
        get { return Title; }
        private set { Title = value ?? throw new ArgumentNullException(nameof(value)); }
    }

    public Media() { }

    public Media(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public abstract override string ToString();
}