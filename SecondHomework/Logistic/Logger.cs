class Logger
{
    public static void AskIfNeedToRemove()
    {
        Console.WriteLine("Would you like to remove some parcels (yes/no)?");
    }

    public static void NoParcels()
    {
        Console.WriteLine("We have no parcels to dispatch.");
    }

    public static void CheckForIdToRemove()
    {
        Console.WriteLine("Enter please id of the parcel: ");
    }

    public static void GetNumberOfParcels()
    {
        Console.WriteLine("How many parcels are you planning to send? ");
    }
}