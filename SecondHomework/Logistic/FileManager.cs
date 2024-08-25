using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

public class FileManager
{
    private const string _filePath = @"Parcels.json";
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async Task SaveAsync(List<Parcel> parcels)
    {
        await _semaphore.WaitAsync();
        try
        {
            var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, json);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<Parcel>> ReadAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<Parcel>();                                       //SaveAsync(new List<Parcel>());
            }
            var json = await File.ReadAllTextAsync(_filePath);
            List<Parcel> parcelsList = JsonConvert.DeserializeObject<List<Parcel>>(json) ?? new List<Parcel>();
            return parcelsList;
        }
        finally
        {
            _semaphore.Release();
        }
            
    }
}