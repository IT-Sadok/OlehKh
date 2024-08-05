class Validator
{
    public bool TryParseInt(string booksReply, out int number)
    {
        return int.TryParse(booksReply, out number);
    }
}
