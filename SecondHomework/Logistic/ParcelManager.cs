using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ParcelManager
{
    private List<Parcel> _unsentParcels;
    private List<Parcel> _deliveredParcels;
    private readonly FileManager _fileManager;

    public ParcelManager(FileManager fileManager)
    {
        _fileManager = fileManager;
        _unsentParcels = new List<Parcel>();
        _deliveredParcels = new List<Parcel>();
    }

    public async Task InitializeAsync()
    {
        _unsentParcels = await _fileManager.ReadAsync();
    }

    private static readonly SemaphoreSlim _fileAccessSemaphore = new SemaphoreSlim(2, 2);

    public enum WeightCategory
    {
        UpTo1Kg,
        UpTo2Kg,
        UpTo5Kg,
        UpTo10Kg,
        UpTo20Kg,
        MoreThan20Kg
    };

    public List<Parcel> GetDeliveredParcels()
    {
        return _deliveredParcels;
    }

    public async Task AddParcelAsync(Parcel parcel)
    {
        _unsentParcels.Add(parcel);
        await _fileManager.SaveParcelsAsync(_unsentParcels);
    }

    public List<Parcel> GetParcels()
    {
        return _unsentParcels;
    }

    public async Task<Result> RemoveParcelAsync(Guid id)
    {
        Result result = new Result();
        try
        {
            Parcel? parcelToRemove = _unsentParcels.Find(p => p.Id == id);
            if (parcelToRemove != null)
            {
                _unsentParcels.Remove(parcelToRemove);
                await _fileManager.SaveParcelsAsync(_unsentParcels);
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

    public string[] GetWeightCategories()
    {
        return Enum.GetNames(typeof(WeightCategory));
    }
    
    public Dictionary<WeightCategory, List<Parcel>> GetParcelsByWeight()
    {
        var parcelsByWeight = new Dictionary<WeightCategory, List<Parcel>>
        {
            { WeightCategory.UpTo1Kg, _unsentParcels.Where(parcel => parcel.Weight <= 1).ToList() },
            { WeightCategory.UpTo2Kg, _unsentParcels.Where(parcel => parcel.Weight > 1 && parcel.Weight <= 2).ToList() },
            { WeightCategory.UpTo5Kg, _unsentParcels.Where(parcel => parcel.Weight > 2 && parcel.Weight <= 5).ToList() },
            { WeightCategory.UpTo10Kg, _unsentParcels.Where(parcel => parcel.Weight > 5 && parcel.Weight <= 10).ToList() },
            { WeightCategory.UpTo20Kg, _unsentParcels.Where(parcel => parcel.Weight > 10 && parcel.Weight <= 20).ToList() },
            { WeightCategory.MoreThan20Kg, _unsentParcels.Where(parcel => parcel.Weight > 20).ToList() }
        };

        return parcelsByWeight;
    }

    public async Task FilterAndProcessParcelsForDeliveryAsync(DateTime startDate, DateTime endDate)
    {
        await _fileAccessSemaphore.WaitAsync();
        try
        {
            string dateFormat = "yyyy-MM-dd";

            var parcelsToDeliver = _unsentParcels.Where(p =>
            {
                if (DateTime.TryParseExact(p.DateOfParcelRegist, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parcelDate))
                {
                    return parcelDate >= startDate && parcelDate <= endDate;
                }
                return false;
            })
            .OrderBy(p => DateTime.ParseExact(p.DateOfParcelRegist, dateFormat, CultureInfo.InvariantCulture))
            .ToList();

            if (parcelsToDeliver.Any())
            {
                _deliveredParcels.AddRange(parcelsToDeliver);

                _unsentParcels.RemoveAll(p => parcelsToDeliver.Contains(p));
                await _fileManager.SaveParcelsAsync(_unsentParcels);
            }
        }
        finally
        {
            _fileAccessSemaphore.Release();
        }
    }
}