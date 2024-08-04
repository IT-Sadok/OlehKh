using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    public static async Task Main(string[] args)
    {
        var fileHandler = new FileHandler();
        var loggerFile = new LoggerFile();
        List<Book> books = new List<Book>();
        var logger = new Logger();
        var validator = new Validator();
        var library = new Library();

        int startedDeposit = 0;
        // Loading and saving books to file
        try
        {
            books = await fileHandler.ReadJsonAsync<List<Book>>(fileHandler._loadFilePath);
            Console.WriteLine("Books have been loaded.");

            await fileHandler.SaveJsonAsync(fileHandler._saveFilePath, books);
            Console.WriteLine("Books have been saved.");
            startedDeposit = books.Count;
        }
        catch (Exception ex)
        {
            loggerFile.LogError(ex, "An error occurred while processing books.");
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        // making a collection of NEW BOOKS according to database file (last 5 records)
        try
        {
            var collectionsOfBooks = new CollectionsOfBooks(books, fileHandler._saveFilePath);
        }
        catch (Exception ex)
        {
            loggerFile.LogError(ex, "An error occurred while creating the collections of books.");
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        int number = 0;

        if (library.GetBalance() == 0) // check if balance zero, so it's our first client
        {
            library.SetBalance(startedDeposit);
            Console.WriteLine("Welcome to our library! What book would you like to take?");
            Console.WriteLine("Clients reply...");
            logger.PrintTakeQuestion(library.Balance);
            if (validator.TryParseInt(logger.GetReplyHowManyBooks(), out number))
            {
                int result = library.Remove(number, library.Balance);
                logger.PrintCalculationResult(result);
            }
            else
            {
                Console.WriteLine("Please, put just a number!");
            }
        }
        else if (library.GetBalance() > 0)
        {
            Console.WriteLine("Welcome to our library! Would you like to take a book or give it back");
            string? actionReply = Console.ReadLine();

            switch (actionReply)
            {
                case "take":
                    if (validator.TryParseInt(logger.GetReplyHowManyBooks(), out number))
                    {
                        int result = library.Remove(number, library.Balance);
                        logger.PrintCalculationResult(result);
                    }
                    else
                    {
                        Console.WriteLine("Please, put just a number!");
                    }
                    break;
                
                case "back":
                    logger.PrintGiveBackQuestion();
                    if (validator.TryParseInt(logger.GetReplyHowManyBooks(), out number))
                    {
                        int result = library.Add(number, library.Balance);
                        logger.PrintCalculationResult(result);
                    }
                    else
                    {
                        Console.WriteLine("Please, put just a number!");
                    }
                    break;
            }
        }
    }
}
