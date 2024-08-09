using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
<<<<<<< HEAD
using System.Xml.Linq;
=======
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
using Newtonsoft.Json;

public class ParcelManager
{
<<<<<<< HEAD
    public string _filePath { get; private set; } = @"C:\Users\kharc\source\repos\SecondHomework\Logistic\Logistic\Parcels.json";

    private List<Parcel> parcels = new List<Parcel>();

    public ParcelManager()
    {

    }

    public string GetPath()
    {
        return _filePath;
=======
    private const string _filePath = @"C:\Users\kharc\source\repos\SecondHomework\Logistic\Logistic\Parcels.json";

    private List<Parcel> parcels;

    public ParcelManager()
    {
        parcels = new List<Parcel>();
    }

    public void AddParcelFromInput()
    {
        Console.Write("What are you planning to send? ");
        string? name = Console.ReadLine();

        Console.Write("Who is the recipient? ");
        string? recipient = Console.ReadLine();

        Console.Write("Enter the destination, please: ");
        string? destination = Console.ReadLine();

        AddParcel(name, recipient, destination);
    }

    public void Display()
    {
        if (parcels.Count == 0)
        {
            Logger.NoParcels();
        }
        else
        {
            foreach (var parcel in parcels)
            {
                Console.WriteLine(parcel);
            }
        }
    }

    public void AddParcel(string? name, string? recipient, string? destination)
    {
        var parcel = new Parcel(Guid.NewGuid(), name, recipient, destination);
        parcels.Add(parcel);
        SaveParcels();
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
    }

    public void SaveParcels()
    {
        var json = JsonConvert.SerializeObject(parcels, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

<<<<<<< HEAD
    public void GetParcelsInfoAndSave()
    {
        string? answer = Console.ReadLine();
=======
    public void CheckIfDeleteAndRemove()
    {
        string? answer = Console.ReadLine();
        if (answer?.ToLower() == "yes")
        {
            Logger.CheckForIdToRemove();
            string? id = Console.ReadLine();
            if (Guid.TryParse(id, out Guid Id))
            {
                DeleteParcels(Id);
                Console.WriteLine("Updated parcels are:");
                Display();
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
        else if (answer?.ToLower() == "no")
        {
            Console.WriteLine("Have a nice day!");
        }
        else if (string.IsNullOrWhiteSpace(answer))
        {
            Console.WriteLine("Don't be silent, i've got a lot of work");
        }
    }

    public void DeleteParcels(Guid id)
    {
        var parcel = parcels.Find(p => p.Id == id);
        if (parcel != null)
        {
            parcels.Remove(parcel);
            SaveParcels();
            Console.WriteLine($"Parcel with ID {id} has been removed.");
        }
        else
        {
            Console.WriteLine($"Parcel with ID {id} not found.");
        }
    }

    public void GetNumberAndSave()
    {
        Logger.GetNumberOfParcels();
        string? answer = Console.ReadLine();
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
        if (int.TryParse(answer, out int number))
        {
            if (number == 1)
            {
<<<<<<< HEAD
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
=======
                AddParcelFromInput();
                Console.WriteLine("Saved");
                SaveParcels();
            }
            else if (answer?.ToLower() == "no")
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
            {
                Console.WriteLine("So, if you have nothing to send why do you here? ");
            }
            else if (string.IsNullOrWhiteSpace(answer))
            {
                Console.WriteLine("Don't be silent, i've got a lot of work");
            }
<<<<<<< HEAD
        }
    }
}
=======
            else if (number > 1)
            {
                for (int i = 0; i < number; i++)
                {
                    AddParcelFromInput();
                    Console.WriteLine("Saved");
                }
                SaveParcels();
            }
        }Display();
    }
}
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
