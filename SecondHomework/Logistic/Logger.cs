class Logger
{
    public void GetNumberOfParcels()
    {
        Console.WriteLine("How many parcels are you planning to send? ");
    }

    public void AskIfNeedToRemove()
    {
        Console.WriteLine("Would you like to remove some parcels (yes/no)?");
    }

    public static void CheckForIdToRemove()
    {
        Console.WriteLine("Enter please id of the parcel: ");
    }

    
}