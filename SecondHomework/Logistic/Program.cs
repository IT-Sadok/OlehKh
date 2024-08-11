using System;
using System.Collections.Generic;

ParcelManager parcelManager = new ParcelManager();
Logger logger = new Logger();
FileManager fileManager = new FileManager();


logger.GetAmountOfParcels();
string? AmountOfParcels = Console.ReadLine();

if (int.TryParse(AmountOfParcels, out int amountOfParcels))
{
    if (amountOfParcels > 0)
    {
        for (int i = 0; i < amountOfParcels; i++)
        {
            Parcel newParcel = GetParcelDetailsFromInput();
            parcelManager.AddParcel(newParcel);
        }
        Console.WriteLine("Parcel added successfully.");
        foreach (var parcel in parcelManager.ReadParcels())
        {
            Console.WriteLine(parcel.ToString());
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

logger.AskIfNeedToRemove();
string? answer = Console.ReadLine();
if (answer == "yes")
{
    logger.CheckForIdToRemove();
    string? ID = Console.ReadLine();
    if (Guid.TryParse(ID, out Guid id))
    {
        List<Parcel> parcel = parcelManager.ReadParcels();
        bool isRemoved = parcelManager.RemoveParcel(id, parcel, out string message);
        Console.WriteLine(message);

        if (isRemoved)
        {
            fileManager.Save(parcel);
        }
        foreach (var parcels in parcelManager.ReadParcels())
        {
            Console.WriteLine(parcels.ToString());
        }
    }
}
else
{
    Console.WriteLine("Okay. Have a nice day!");
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

    return new Parcel(Guid.NewGuid(), name, recipient, destination);
}
