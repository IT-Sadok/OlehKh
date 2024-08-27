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
    private static readonly SemaphoreSlim _fileAccessSemaphore = new SemaphoreSlim(2, 2); // Обмеження на 2 потоки
    private static readonly SemaphoreSlim _deliverySemaphore = new SemaphoreSlim(2, 2); // Для відправки посилок

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

    public async Task FilteringParcelsForDeliveryAsync()
    {
        await _fileAccessSemaphore.WaitAsync();
        try
        {
            var unsentParcels = await ReadAsync();

            Console.WriteLine("Enter the start date (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine();
            Console.WriteLine("Enter the end date (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine();

            string dateFormat = "yyyy-MM-dd";

            if (DateTime.TryParseExact(startDateInput, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                DateTime.TryParseExact(endDateInput, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {

                // Фільтрація посилок для доставки (дата останні два дні)
                var parcelsToDeliver = unsentParcels.Where(p =>
                {
                    if (DateTime.TryParseExact(p.DateOfParcelRegist, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parcelDate))
                    {
                        return parcelDate >= startDate && parcelDate < endDate;
                    }
                    return false;
                })
                .OrderBy(p => DateTime.ParseExact(p.DateOfParcelRegist, dateFormat, CultureInfo.InvariantCulture))
                .ToList();

                if (parcelsToDeliver.Any())
                {
                    var deliveredParcels = await ReadDeliveredAsync();
                    deliveredParcels.AddRange(parcelsToDeliver);
                    await SaveDeliveredAsync(deliveredParcels);

                    // Видалення відправлених посилок з невідправлених
                    unsentParcels.RemoveAll(p => parcelsToDeliver.Contains(p));
                    await SaveParcelsAsync(unsentParcels);
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
            }
        }
        finally
        {
            _fileAccessSemaphore.Release();
        }
    }
}