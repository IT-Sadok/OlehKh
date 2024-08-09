/*
class DeletingParcels
{
    public void CheckIfDeleteAndRemove()
    {
        string? answer = Console.ReadLine();
        if (answer?.ToLower() == "yes")
        {
            Logger.CheckForIdToRemove();
            string? id = Console.ReadLine();
            if (Guid.TryParse(id, out Guid Id))
            {
                DeleteParcels(Id);
                Console.WriteLine("Updated parcels are:");
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
        else if (answer?.ToLower() == "no")
        {
            Console.WriteLine("Have a nice day!");
        }
        else if (string.IsNullOrWhiteSpace(answer))
        {
            Console.WriteLine("Don't be silent, i've got a lot of work");
        }
    }

    public void DeleteParcels(Guid id)
    {
        var parcel = parcels.Find(p => p.Id == id);
        if (parcel != null)
        {
            parcels.Remove(parcel);
            SaveParcels();
            Console.WriteLine($"Parcel with ID {id} has been removed.");
        }
        else
        {
            Console.WriteLine($"Parcel with ID {id} not found.");
        }
    }
}
}
*/