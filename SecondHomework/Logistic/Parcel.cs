<<<<<<< HEAD
﻿using System.Collections.Generic;
using System;
=======
﻿using System;
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66

public class Parcel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Recipient { get; private set; }
    public string Destination { get; private set; }

    public Parcel(Guid id, string name, string recipient, string destination)
    {
        Id = id;
        Name = name;
        Recipient = recipient;
        Destination = destination;
    }

<<<<<<< HEAD
    public static Parcel GetParcelDetailsFromInput()
    {
        Console.Write("What are you planning to send? ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.");

        Console.Write("Who is the recipient? ");
        string? recipient = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(recipient)) throw new ArgumentException("Recipient cannot be null or empty.");

        Console.Write("Enter the destination, please: ");
        string? destination = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(destination)) throw new ArgumentException("Destination cannot be null or empty.");

        return new Parcel(Guid.NewGuid(), name, recipient, destination);
    }

=======
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Recipient: {Recipient}, Destination: {Destination}";
    }
}