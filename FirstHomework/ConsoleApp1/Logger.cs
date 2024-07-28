class Logger
{
    // first message for taking book
    public void PrintTakeQuestion()
    {
        Console.WriteLine("How many books do you want to take? Our library have a huge amount of books. It's 10 000 items!");
    }

    // first message for giving back book
    public void PrintGiveBackQuestion()
    {
        Console.WriteLine("How many books do you want to give back");
    }

    // getting reply how many books
    public string GetReplyHowManyBooks()
    {
        string? booksReply = Console.ReadLine();
        return booksReply;
    }

    // getting a list of books
    public void GetListOfBooks()
    {
        Console.WriteLine("So, we've received the following books from you:");
    }

    // giving a list of the best books
    public void GiveListOfBestBooks()
    {
        Console.WriteLine("Let me give you a list of the best books to get with you for the last week:");

    }

    // second message for taking book
    public void PrintRemoveMessage(int a, int result)
    {
        Console.WriteLine($"Thank You. You take {a} book(s), and our depositary for now is {result} items!");
        Console.WriteLine("Have a good time! See you back soon!");
    }

    // second message for giving back book
    public void PrintAddMessage(int b, int result)
    {
        Console.WriteLine($"Thank You. You give us back {b} book(s), and our depositary for now is {result} items!");
        Console.WriteLine("See you back soon!");
    }

    // finale message with calculating
    public void PrintCalculationResult(int result)
    {
        Console.WriteLine($"Thank you! For now, the amount of books is {result}. See you soon.");
    }
}