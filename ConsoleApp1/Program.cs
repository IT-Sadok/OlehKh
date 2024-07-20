LibraryCount reader1 = new LibraryCount();
Messages Mes1 = new Messages();
OperatingFigures Fig1 = new OperatingFigures();

Console.WriteLine("Welcome to our library! Do you want to take a book or give it back?");

string actionReply = Console.ReadLine();

switch (actionReply)
{
    case "take":
        Mes1.QuestionIfTake();
        string booksReply = Console.ReadLine();
        bool a = Fig1.IsInt(booksReply, out int number);
        if (a)
        {
            int result = reader1.Remove(number, reader1.GetStartedDeposit());
            Mes1.CalculatingResult(result);
        }
        else
        {
            Console.WriteLine("Please, put just a number!");
        }
        break;

    case "back":
        Mes1.QuestionIfGiveBack();
        string booksReply2 = Console.ReadLine();
        bool b = Fig1.IsInt(booksReply2, out int number2);
        if (b)
        {
            int result = reader1.Add(number2, reader1.GetStartedDeposit());
            Mes1.CalculatingResult(result);
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