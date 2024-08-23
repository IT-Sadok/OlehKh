using System;

public class Parcel: ParcelBase
{
    public Parcel(Guid id, string name, string recipient, string destination, string dateOfParcelRegist, float weight,
        float shippingCost) :
        base(id, name, recipient, destination, dateOfParcelRegist, weight, shippingCost)
    {

    }
}