using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class FileManager
{
    private const string _filePath = @"C:\Users\kharc\source\repos\SecondHomework\Logistic\Parcels.json";

    public void Save(List<Parcel> parcels)
    {
        try
        {
            var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving parcels to file: {ex.Message}");
        }
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

    public void Remove(Guid id, List<Parcel> parcelsList)
    {
        try
        {
            Parcel parcelToRemove = parcelsList.Find(p => p.Id == id);
            if (parcelToRemove != null)
            {
                parcelsList.Remove(parcelToRemove);
                Save(parcelsList);
                Console.WriteLine($"Parcel with ID: {id} removed successfully.");
            }
            else
            {
                Console.WriteLine($"Parcel with ID: {id} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving parcels to file: {ex.Message}");
        }
    }

    public void DisplayParcels()
    {
        List<Parcel> parcels = Read();

        if (parcels.Count > 0)
        {
            foreach (var parcel in parcels)
            {
                Console.WriteLine(parcel.ToString());
            }
        }
        else
        {
            Console.WriteLine("No parcels to display.");
        }
    }
}