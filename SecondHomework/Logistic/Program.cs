using System;

ParcelManager parcelManager = new ParcelManager();
Logger logger = new Logger();


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
        parcelManager.SaveParcels();
        Console.WriteLine("Parcel added successfully.");
        parcelManager.DisplayParcels();
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
        parcelManager.RemoveParcels(id);
        parcelManager.SaveParcels();
        parcelManager.DisplayParcels();
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
