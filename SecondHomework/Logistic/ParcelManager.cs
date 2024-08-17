public class ParcelManager
{

    private List<Parcel> _parcels = new List<Parcel>();
    private FileManager _fileManager = new FileManager();
    private readonly string[] _weightCategories =
    {
        "Up to 1 kg",
        "Up to 2 kg",
        "Up to 5 kg",
        "Up to 10 kg",
        "Up to 20 kg",
        "More than 20 kg"
    };

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

    public List<List<Parcel>> GetParcelsByWeight()
    {
        var upTo1Kg = _parcels.Where(parcel => parcel.Weight <= 1).ToList();
        var upTo2Kg = _parcels.Where(parcel => parcel.Weight > 1 && parcel.Weight <= 2).ToList();
        var upTo5Kg = _parcels.Where(parcel => parcel.Weight > 2 && parcel.Weight <= 5).ToList();
        var upTo10Kg = _parcels.Where(parcel => parcel.Weight > 5 && parcel.Weight <= 10).ToList();
        var upTo20Kg = _parcels.Where(parcel => parcel.Weight > 10 && parcel.Weight <= 20).ToList();
        var moreThan20Kg = _parcels.Where(parcel => parcel.Weight > 20).ToList();

        return new List<List<Parcel>> { upTo1Kg, upTo2Kg, upTo5Kg, upTo10Kg, upTo20Kg, moreThan20Kg };
    }
    public string[] GetWeightCategories()
    {
        return _weightCategories;
    }

}