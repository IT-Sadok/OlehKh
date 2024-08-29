using System;

public class Parcel: ParcelBase
{
    public Parcel(Guid id, string category, string recipient, string destination, string dateOfParcelRegist, float weight,
        float shippingCost) :
        base(id, category, recipient, destination, dateOfParcelRegist, weight, shippingCost)
    {
    }
}