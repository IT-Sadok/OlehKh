using Newtonsoft.Json;

class FileManager: ParcelManager
{
    private const string _filePath = @"C:\Users\kharc\source\repos\SecondHomework\Logistic\Parcels.json";
    private List<Parcel> parcels;

    public FileManager(List<Parcel> parcelsList)
    {
        parcels = parcelsList;
    }

    public void Save(List<Parcel> parcels)
    {
        var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public List<Parcel> Read()
    {
        try
        {
            var json = File.ReadAllText(_filePath);
            List<Parcel> parcelsList = JsonConvert.DeserializeObject<List<Parcel>>(json) ?? new List<Parcel>();
            return parcelsList;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
            return new List<Parcel>();
        }
    }

    public void Remove(Guid id)
    {
        parcels = Read();
        var parcel = parcels.Find(p => p.Id == id);
        if (parcel != null)
        {
            parcels.Remove(parcel);
            Save(parcels);
            Console.WriteLine($"Parcel with ID {id} has been removed.");
            Console.WriteLine("Updated parcels are:");
            DisplayParcels(parcels);
        }
        else
        {
            Console.WriteLine($"Parcel with ID {id} not found.");
        }

    }

    public void CheckIfDeleteAndRemove()
    {
        string? answer = Console.ReadLine();
        if (answer?.ToLower() == "yes")
        {
            Logger.CheckForIdToRemove();
            string? id = Console.ReadLine();
            if (Guid.TryParse(id, out Guid Id))
            {
                Remove(Id);
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
}









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