using Newtonsoft.Json;
using System.Collections.Generic;

ParcelManager parcelManager = new ParcelManager();
Logger logger = new Logger();

FileManager fileManager = new FileManager(new List<Parcel>());
List<Parcel> parcels = fileManager.Read();
fileManager = new FileManager(parcels);

logger.GetNumberOfParcels();
parcelManager.GetParcelsInfoAndSave();
logger.AskIfNeedToRemove();
fileManager.CheckIfDeleteAndRemove();




// parcelManager.DisplayParcels();
// logger.AskIfNeedToRemove();





// reaad json file
// display the result
// ask if want to delete
// delete logic
// reaad json file
// display the result
