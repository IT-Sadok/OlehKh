using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class FileManager
{
    private const string _filePath = @"Parcels.json";
    private static readonly SemaphoreSlim _deliverySemaphore = new SemaphoreSlim(2, 2);

    public async Task SaveParcelsAsync(List<Parcel> parcels)
    {
        var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<List<Parcel>> ReadAsync()
    {

        if (!File.Exists(_filePath))
        {
            return new List<Parcel>();
        }
        var json = await File.ReadAllTextAsync(_filePath);
        List<Parcel> parcelsList = JsonConvert.DeserializeObject<List<Parcel>>(json) ?? new List<Parcel>();
        return parcelsList;
    }
}