public class ParcelManager
{

    private List<Parcel> parcels = new List<Parcel>();
    private FileManager fileManager = new FileManager();

    public ParcelManager()
    {
        parcels = new List<Parcel>();
    }

    public ParcelManager(FileManager fileManager)
    {
        this.fileManager = fileManager;
        parcels = fileManager.Read();
    }

    public void AddParcel(Parcel parcel)
    {
        parcels.Add(parcel);
        fileManager.Save(parcels);
    }

    public List<Parcel> ReadParcels()
    {
        return parcels;
    }

    public bool RemoveParcel(Guid id, List<Parcel> parcelsList, out string message)
    {
        try
        {
            Parcel? parcelToRemove = parcelsList.Find(p => p.Id == id);
            if (parcelToRemove != null)
            {
                parcelsList.Remove(parcelToRemove);
                fileManager.Save(parcelsList);
                message = $"Parcel with ID: {id} removed sucessfully.";
                return true;
            }
            else
            {
                throw new Exception($"Parcel with ID: {id} not found.");
            }
        }
        catch (Exception ex)
        {
            message = $"Error saving parcels to file: {ex.Message}";
            return false;
        }
    }
}