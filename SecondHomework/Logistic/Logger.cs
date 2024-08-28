using static ParcelManager;

class Logger
{
    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintSortedParcelsByWeight(Dictionary<WeightCategory, List<Parcel>> sortedParcels)
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

    public void PrintDeliveredParcels(IEnumerable<Parcel> parcels)
    {
        PrintMessage("Delivered Parcels:");
        foreach (var parcel in parcels)
        {
            PrintMessage(parcel.ToString());
        }
    }

    public bool TryReadConfirmation(Action messageAction)
    {
        messageAction();
        return CheckIfYes();
    }

    private bool CheckIfYes()
    {
        string? answer = Console.ReadLine();
        return answer?.ToLower() == "yes";
    }
}