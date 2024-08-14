public class ParcelManager
{

    private List<Parcel> _parcels = new List<Parcel>();
    private FileManager _fileManager = new FileManager();

    public ParcelManager()
    {
        _parcels = new List<Parcel>();
    }

    public ParcelManager(FileManager fileManager)
    {
        _fileManager = fileManager;
        _parcels = fileManager.Read();
    }

    public void AddParcel(Parcel parcel)
    {
        _parcels.Add(parcel);
        _fileManager.Save(_parcels);
    }

    public List<Parcel> GetParcels()
    {
        return _parcels;
    }

    public Result RemoveParcel(Guid id, List<Parcel> parcelsList)
    {   
        Result result = new Result();
        try
        {
            Parcel? parcelToRemove = parcelsList.Find(p => p.Id == id);
            if (parcelToRemove != null)
            {
                parcelsList.Remove(parcelToRemove);
                _fileManager.Save(parcelsList);
                result.SetResult(true, $"Parcel with ID: {id} removed successfully.");
            }
            else
            {
                result.SetResult(false, $"Parcel with ID: {id} not found.");
            }
        }
        catch (Exception ex)
        {
            result.SetResult(false, $"Error saving parcels to file: {ex.Message}");
        }
        return result;
    }
}