﻿using static ParcelManager;

class Logger
{
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void FilteredParcels()
    {
        Console.WriteLine("Here are parcels filtered by weight:");
    }

    public void PrintSortedParcels(Dictionary<WeightCategory, List<Parcel>> sortedParcels)
    {
        foreach (var category in sortedParcels)
        {
            Console.WriteLine(category.Key + ":");
            foreach (var parcel in category.Value)
            {
                Console.WriteLine(parcel.ToString());
            }
        }
    }

    public bool TryReadConfirmation(Action messageAction)
    {
        messageAction(); // Виклик делегата для виведення повідомлення
        return CheckIfYes(); // Перевірка відповіді
    }

    // Метод для перевірки відповіді користувача
    private bool CheckIfYes()
    {
        string? answer = Console.ReadLine();
        return answer?.ToLower() == "yes";
    }
}