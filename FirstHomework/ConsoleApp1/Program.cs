CalculationBooksFlow reader1 = new CalculationBooksFlow();
Logger Log1 = new Logger();
Validator Fig1 = new Validator();
Library library1 = new Library();
Top5BooksToGet Top5 = new Top5BooksToGet();

Console.WriteLine("Welcome to our library! Do you want to take a book or give it back?");

string? actionReply = Console.ReadLine();

int number; // for further calculations(number of books)

switch (actionReply)
{
    case "take":
        Log1.PrintTakeQuestion();        ;
        if (Fig1.TryParseInt(Log1.GetReplyHowManyBooks(), out number))
        {
            int result = reader1.Remove(number);
            Log1.GiveListOfBestBooks();
            Top5.PrintBooks();
            Log1.PrintCalculationResult(result);
        }
        else
        {
            Console.WriteLine("Please, put just a number!");
        }
        break;


    case "back":
        Log1.PrintGiveBackQuestion();
        if (Fig1.TryParseInt(Log1.GetReplyHowManyBooks(), out number))
        {
            int result = reader1.Add(number);
            Log1.GetListOfBooks();
            library1.AddBooksInfo(new Book("On the Origin of Species", "Charles Darwin"));
            library1.AddBooksInfo(new Book("The Great Gatsby", "F SCOTT FITZGERALD"));
            library1.AddBooksInfo(new Book("The Catcher In The Rye", "J.D. SALINGER"));
            library1.AddBooksInfo(new Book("1984", "GEORGE ORWEL"));
            library1.AddBooksInfo(new Book("Lord Of The Rings Trilogy", "J.R.R. TOLKIEN"));
            library1.DisplayBooks();

            Log1.PrintCalculationResult(result);
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