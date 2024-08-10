using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

public class ParcelManager
{

    private List<Parcel> parcels = new List<Parcel>();

    public ParcelManager()
    {
        parcels = new List<Parcel>();
    }

    public void GetParcelsInfoAndSave()
    {
        string? answer = Console.ReadLine();
        if (int.TryParse(answer, out int number))
        {
            if (number >= 1)
            {
                for (int i = 0; i < number; i++)
                {
                    parcels.Add(Parcel.GetParcelDetailsFromInput());
                    Console.WriteLine("Saved");
                }
                FileManager fileManager = new FileManager(parcels);
                fileManager.Save(parcels);
                parcels = fileManager.Read();
                DisplayParcels(parcels);
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

    public virtual void DisplayParcels(List<Parcel>parcels)
    {
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