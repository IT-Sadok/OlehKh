using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_sadok_hw
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Library reader1 = new Library(); // new object
            Console.WriteLine("Welcome to our library! Do you want to take a book or you want to give it back?");
            string reply = Console.ReadLine(); // receiving a reader's reply

            // logic if reader wants to take a book
            if (reply.Contains("take"))
            {
                Console.WriteLine("How many books do you want to take? Our library have a huge amount of books. It's 10 000 items!");
                int ReplyTake = Int32.Parse(Console.ReadLine());
                reader1.Add(ReplyTake); // call method from class Library to decrease amount of books
            }
            // logic if reader wants to give it back
            if (reply.Contains("back"))
            {
                Console.WriteLine("How many books do you want to give back?");
                int ReplyGive = Int32.Parse(Console.ReadLine());
                reader1.Remove(ReplyGive); // call method from class Library to increase amount of books
            }
        }
    }
}