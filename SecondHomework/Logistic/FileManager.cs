using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

public class FileManager
{
    private const string _filePath = @"Parcels.json";
    private const string _deliveredParcelsFilePath = @"DeliveredParcels.json";
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

    public async Task<List<Parcel>> ReadDeliveredAsync()
    {
        await _deliverySemaphore.WaitAsync(); // Захист доступу до файлу DeliveredParcels.json
        try
        {
            if (!File.Exists(_deliveredParcelsFilePath))
            {
                // Якщо файл не існує, створюємо новий файл з пустим списком
                await File.WriteAllTextAsync(_deliveredParcelsFilePath, "[]");
                return new List<Parcel>(); // Повертаємо пустий список, якщо файл щойно створено
            }

            var json = await File.ReadAllTextAsync(_deliveredParcelsFilePath);
            return JsonConvert.DeserializeObject<List<Parcel>>(json) ?? new List<Parcel>();
        }
        finally
        {
            _deliverySemaphore.Release();
        }
    }

    public async Task SaveDeliveredAsync(List<Parcel> parcels)
    {
        await _deliverySemaphore.WaitAsync(); // Захист доступу до файлу DeliveredParcels.json
        try
        {
            var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(_deliveredParcelsFilePath, json);
        }
        finally
        {
            _deliverySemaphore.Release();
        }
    }
}