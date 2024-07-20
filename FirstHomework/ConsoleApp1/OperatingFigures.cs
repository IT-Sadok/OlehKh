class OperatingFigures
{
    public bool IsInt(string booksReply, out int number)
    {
        return int.TryParse(booksReply, out number);
    }
}
