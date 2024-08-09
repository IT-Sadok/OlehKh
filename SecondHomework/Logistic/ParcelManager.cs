using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

public class ParcelManager
{
    public string _filePath { get; private set; } = @"C:\Users\kharc\source\repos\SecondHomework\Logistic\Logistic\Parcels.json";

    private List<Parcel> parcels = new List<Parcel>();

    public ParcelManager()
    {

    }

    public string GetPath()
    {
        return _filePath;
    }

    public void SaveParcels()
    {
        var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void GetParcelsInfoAndSave()
    {
        string? answer = Console.ReadLine();
        if (int.TryParse(answer, out int number))
        {
            if (number == 1)
            {
                parcels.Add(Parcel.GetParcelDetailsFromInput());
                SaveParcels();
                Console.WriteLine("Saved");
            }
            else if (number > 1)
            {
                for (int i = 0; i < number; i++)
                {
                    parcels.Add(Parcel.GetParcelDetailsFromInput());
                    Console.WriteLine("Saved");
                }
                SaveParcels();
            }
            else if (number == 0)
            {
                Console.WriteLine("So, if you have nothing to send why do you here? ");
            }
            else if (string.IsNullOrWhiteSpace(answer))
            {
                Console.WriteLine("Don't be silent, i've got a lot of work");
            }
        }
    }
}