using System.Collections.Generic;
using System;

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

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Recipient: {Recipient}, Destination: {Destination}";
    }
}