﻿using System;
using System.Collections.Generic;
using System.Globalization;
using static ParcelManager;

Logger logger = new Logger();
FileManager fileManager = new FileManager();
ParcelManager parcelManager = new ParcelManager(fileManager);
string[] weightCategories = parcelManager.GetWeightCategories();

logger.PrintMessage("How many parcels are you planning to send? ");
string? AmountOfParcels = Console.ReadLine();


if (int.TryParse(AmountOfParcels, out int amountOfParcels))
{
    if (amountOfParcels > 0)
    {
        for (int i = 0; i < amountOfParcels; i++)
        {
            try
            {
                Parcel newParcel = GetParcelDetailsFromInput();
                parcelManager.AddParcel(newParcel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        Console.WriteLine("Parcel added successfully.");
        try
        {
            foreach (var parcel in parcelManager.GetParcels())
            {
                Console.WriteLine(parcel.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading parcels: {ex.Message}");    
        }
    }
    else
    {
        Console.WriteLine("No parcels to add.");
    }
}

else
{
    Console.WriteLine("Invalid input. Please enter a valid number.");
}

logger.PrintMessage("Would you like to remove some parcels (yes/no)?");

if (CheckIfYes())
{
    logger.PrintMessage("Enter please id of the parcel: ");
    string? ID = Console.ReadLine();
    if (Guid.TryParse(ID, out Guid id))
    {
        List<Parcel> parcel = parcelManager.GetParcels();
        Result result = parcelManager.RemoveParcel(id, parcel);
        bool isRemoved = result.Success;
        Console.WriteLine(result.Message);

        if (isRemoved)
        {
            fileManager.Save(parcel);
        }
        foreach (var parcels in parcelManager.GetParcels())
        {
            Console.WriteLine(parcels.ToString());
        }
    }
}

logger.PrintMessage("Would you like to get a list of parcels filtered by weight?");

if (CheckIfYes())
{
    Console.WriteLine("Here are parcels filtered by weight:");
    Dictionary<WeightCategory, List<Parcel>> sortedParcels = parcelManager.GetParcelsByWeight();

    foreach (var category in sortedParcels)
    {
        Console.WriteLine(category.Key + ":");
        foreach (var parcel in category.Value)
        {
            Console.WriteLine(parcel.ToString());
        }
    }
}



Parcel GetParcelDetailsFromInput()
{
    Console.Write("What are you planning to send? ");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.");

    Console.Write("Who is the recipient? ");
    string? recipient = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(recipient)) throw new ArgumentException("Recipient cannot be null or empty.");

    Console.Write("Enter the destination, please: ");
    string? destination = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(destination)) throw new ArgumentException("Destination cannot be null or empty.");

    Console.Write("Please enter a date you want your parcel to be shipped. Date should in format: yyyy-MM-dd: ");
    string? dateOfParcelRegist = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(dateOfParcelRegist)) throw new ArgumentException("Data cannot be null or empty.");

    Console.Write("Please enter a weight of your parcel: ");
    string? weightInput = Console.ReadLine();
    if (!float.TryParse(weightInput, out float weight)) throw new ArgumentException("Weight of the parcel cannot be null or empty.");

    Console.Write("How much it will be to ship your parcel? ");
    string? shippingCostInput = Console.ReadLine();
    if (!float.TryParse(shippingCostInput, out float shippingCost)) throw new ArgumentException("Shipping cost of the parcel cannot be null or empty.");

    return new Parcel(Guid.NewGuid(), name, recipient, destination, dateOfParcelRegist, weight, shippingCost);
}

bool CheckIfYes()
{
    string? answer = Console.ReadLine();
    return answer?.ToLower() == "yes";
}
