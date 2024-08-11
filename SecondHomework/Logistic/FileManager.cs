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
            throw new Exception($"Error saving parcels to file: {ex.Message}", ex);
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
            throw new Exception($"Error saving parcels to file: {ex.Message}", ex);
        }
    }
}