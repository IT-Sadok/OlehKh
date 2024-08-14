using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class FileManager
{
    private const string _filePath = @"Parcels.json";

    public void Save(List<Parcel> parcels)
    {
        var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public List<Parcel> Read()
    {
        if (!File.Exists(_filePath))
        {
            Save(new List<Parcel>());
        }
        var json = File.ReadAllText(_filePath);
        List<Parcel> parcelsList = JsonConvert.DeserializeObject<List<Parcel>>(json) ?? new List<Parcel>();
        return parcelsList;
    }
}