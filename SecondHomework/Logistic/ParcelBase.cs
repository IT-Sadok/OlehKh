using System;

public abstract class ParcelBase
{
    public Guid Id { get; private set; }
    public string Category { get; private set; }
    public string Recipient { get; private set; }
    public string Destination { get; private set; }
    public string DateOfParcelRegist { get; private set; }
    public float Weight { get; private set; }
    public float ShippingCost { get; private set; }

    public ParcelBase(Guid id, string category, string recipient, string destination, string dateOfParcelRegist, float weight,
        float shippingCost)
    {
        Id = id;
        Category = category;
        Recipient = recipient;
        Destination = destination;
        DateOfParcelRegist = dateOfParcelRegist;
        Weight = weight;
        ShippingCost = shippingCost;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Category: {Category}, Recipient: {Recipient}, Destination: {Destination}," +
            $"DateOfParcelRegist: {DateOfParcelRegist}, Weight: {Weight}";
    }
}