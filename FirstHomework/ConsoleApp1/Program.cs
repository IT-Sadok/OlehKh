LibraryCount reader1 = new LibraryCount();
Messages Mes1 = new Messages();
Validator Fig1 = new Validator();

Console.WriteLine("Welcome to our library! Do you want to take a book or give it back?");

string actionReply = Console.ReadLine();

switch (actionReply)
{
    case "take":
        Mes1.PrintTakeQuestion();
        string booksReply = Console.ReadLine();
        bool isInt1 = Fig1.TryParseInt(booksReply, out int number);
        if (isInt1)
        {
            int result = reader1.Remove(number, reader1.Deposit);
            Mes1.PrintCalculationResult(result);
        }
        else
        {
            Console.WriteLine("Please, put just a number!");
        }
        break;

    case "back":
        Mes1.PrintGiveBackQuestion();
        string booksReply2 = Console.ReadLine();
        bool isInt2 = Fig1.TryParseInt(booksReply2, out int number2);
        if (isInt2)
        {
            int result = reader1.Add(number2, reader1.Deposit);
            Mes1.PrintCalculationResult(result);
        }
        else
        {
            Console.WriteLine("Please, put just a number!");
        }
        break;

    default:
        Console.WriteLine("Sorry! What did you just say?");
        break;
}