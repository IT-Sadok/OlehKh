<<<<<<< HEAD
﻿using Newtonsoft.Json;

ParcelManager parcelManager = new ParcelManager();
Logger logger = new Logger();

logger.GetNumberOfParcels();
parcelManager.GetParcelsInfoAndSave();




// reaad json file
// display the result
// ask if want to delete
// delete logic
// reaad json file
// display the result
=======
﻿ParcelManager parcelManager = new ParcelManager();


parcelManager.GetNumberAndSave();
Logger.AskIfNeedToRemove();
parcelManager.CheckIfDeleteAndRemove();
>>>>>>> 1fe32bed81a8839ba1ec30533e33c95539a4bb66
