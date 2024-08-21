public class ParcelManager
{

    private List<Parcel> _parcels = new List<Parcel>();
    private FileManager _fileManager = new FileManager();
    public enum WeightCategory
    {
        UpTo1Kg,
        UpTo2Kg,
        UpTo5Kg,
        UpTo10Kg,
        UpTo20Kg,
        MoreThan20Kg
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

    public Dictionary<WeightCategory, List<Parcel>> GetParcelsByWeight()
    {
        var parcelsByWeight = new Dictionary<WeightCategory, List<Parcel>>
    {
        { WeightCategory.UpTo1Kg, _parcels.Where(parcel => parcel.Weight <= 1).ToList() },
        { WeightCategory.UpTo2Kg, _parcels.Where(parcel => parcel.Weight > 1 && parcel.Weight <= 2).ToList() },
        { WeightCategory.UpTo5Kg, _parcels.Where(parcel => parcel.Weight > 2 && parcel.Weight <= 5).ToList() },
        { WeightCategory.UpTo10Kg, _parcels.Where(parcel => parcel.Weight > 5 && parcel.Weight <= 10).ToList() },
        { WeightCategory.UpTo20Kg, _parcels.Where(parcel => parcel.Weight > 10 && parcel.Weight <= 20).ToList() },
        { WeightCategory.MoreThan20Kg, _parcels.Where(parcel => parcel.Weight > 20).ToList() }
    };

        return parcelsByWeight;
    }
    public string[] GetWeightCategories()
    {
        return Enum.GetValues(typeof(WeightCategory))
               .Cast<WeightCategory>()
               .Select(wc => wc.ToString())
               .ToArray();
    }

}




