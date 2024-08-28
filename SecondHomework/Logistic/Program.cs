using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using static ParcelManager;

var logger = new Logger();
var fileManager = new FileManager();
var parcelManager = new ParcelManager(fileManager);
await parcelManager.InitializeAsync();

string[] weightCategories = parcelManager.GetWeightCategories();

// adding new parcels
logger.PrintMessage("How many parcels are you planning to send? ");
string? AmountOfParcels = Console.ReadLine();

if (int.TryParse(AmountOfParcels, out int amountOfParcels))
{
    if (amountOfParcels > 0)
    {
        var tasks = new List<Task>();
        for (int i = 0; i < amountOfParcels; i++)
        {
            Parcel newParcel = GetParcelDetailsFromInput();
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await parcelManager.AddParcelAsync(newParcel);
                }
                catch (Exception ex)
                {
                    logger.PrintMessage($"An error occurred: {ex.Message}");
                }
            }));
            
        }
        await Task.WhenAll(tasks);
        logger.PrintMessage("Parcel added successfully.");

        try
        {
            var parcels = await parcelManager.GetParcelsAsync();
            foreach (var parcel in parcels)
            {
                logger.PrintMessage(parcel.ToString());
            }
        }
        catch (Exception ex)
        {
            logger.PrintMessage($"An error occurred while reading parcels: {ex.Message}");    
        }
    }
    else
    {
        logger.PrintMessage("No parcels to add.");
    }
}
else
{
    logger.PrintMessage("Invalid input. Please enter a valid number.");
}

// deleting parcels
bool confirmationToRemove = logger.TryReadConfirmation(() => logger.PrintMessage("Would you like to remove some parcels (yes/no)?"));

if (confirmationToRemove)
{
    logger.PrintMessage("Enter please id of the parcel: ");
    string? ID = Console.ReadLine();
    if (Guid.TryParse(ID, out Guid id))
    {
        List<Parcel> parcels = await parcelManager.GetParcelsAsync();
        Result result = await parcelManager.RemoveParcelAsync(id);
        bool isRemoved = result.Success;
        logger.PrintMessage(result.Message ?? "No message provided.");

        if (isRemoved)
        {
            await fileManager.SaveParcelsAsync(parcels);
            foreach (var parcel in parcels)
            {
                logger.PrintMessage(parcel.ToString());
            }
        }
    }
}

// grouping parcels by weight
bool confirmationToFilter = logger.TryReadConfirmation(() => logger.PrintMessage("Would you like to get a list of parcels filtered by weight?"));

if (confirmationToFilter)
{
    logger.PrintMessage("Here are parcels filtered by weight:");
    Dictionary<WeightCategory, List<Parcel>> sortedParcelsByWeight = parcelManager.GetParcelsByWeight();
    logger.PrintSortedParcelsByWeight(sortedParcelsByWeight);
}
else
{
    logger.PrintMessage("");
}

// starting a delivery process
bool confirmationToDelivery = logger.TryReadConfirmation(() => logger.PrintMessage("Would you like to start delivery process?"));

if (confirmationToDelivery)
{
    logger.PrintMessage("Enter the start date (yyyy-MM-dd): ");
    string? startDateInput = Console.ReadLine();
    logger.PrintMessage("Enter the end date (yyyy-MM-dd): ");
    string? endDateInput = Console.ReadLine();
    string dateFormat = "yyyy-MM-dd";

    if (DateTime.TryParseExact(startDateInput, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
        DateTime.TryParseExact(endDateInput, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
    {
        await parcelManager.FilterAndProcessParcelsForDeliveryAsync(startDate, endDate);
        logger.PrintMessage("Parcel processing for delivery is complete.");
    }
    else
    {
        logger.PrintMessage("Invalid date format. Please use yyyy-MM-dd.");
    }
    if (parcelManager != null)
    {
        logger.PrintDeliveredParcels(parcelManager.GetDeliveredParcels());
    }
    else
    {
        logger.PrintMessage("No parcels ot deliver.");
    }
}


Parcel GetParcelDetailsFromInput()
{
    logger.PrintMessage("What are you planning to send? ");
    string? name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.");

    logger.PrintMessage("Who is the recipient? ");
    string? recipient = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(recipient)) throw new ArgumentException("Recipient cannot be null or empty.");

    logger.PrintMessage("Enter the destination, please: ");
    string? destination = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(destination)) throw new ArgumentException("Destination cannot be null or empty.");

    logger.PrintMessage("Please enter a date you want your parcel to be shipped. Date should in format: yyyy-MM-dd: ");
    string? dateOfParcelRegist = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(dateOfParcelRegist)) throw new ArgumentException("Data cannot be null or empty.");

    logger.PrintMessage("Please enter a weight of your parcel: ");
    string? weightInput = Console.ReadLine();
    if (!float.TryParse(weightInput, out float weight)) throw new ArgumentException("Weight of the parcel cannot be null or empty.");

    logger.PrintMessage("How much it will be to ship your parcel? ");
    string? shippingCostInput = Console.ReadLine();
    if (!float.TryParse(shippingCostInput, out float shippingCost)) throw new ArgumentException("Shipping cost of the parcel cannot be null or empty.");

    return new Parcel(Guid.NewGuid(), name, recipient, destination, dateOfParcelRegist, weight, shippingCost);
}