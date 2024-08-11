public class ParcelManager
{

    private List<Parcel> parcels = new List<Parcel>();
    private FileManager fileManager = new FileManager();

    public ParcelManager()
    {
        parcels = new List<Parcel>();
    }

    public void AddParcel(Parcel parcel)
    {
        parcels.Add(parcel);
    }

    public void SaveParcels()
    {
        fileManager.Save(parcels);
    }

    public List<Parcel> ReadParcels()
    {
        parcels = fileManager.Read();
        return parcels;
    }

    public void RemoveParcels(Guid id)
    {
        fileManager.Remove(id, parcels);
    }

    public void DisplayParcels()
    {
        fileManager.DisplayParcels();
    }
}