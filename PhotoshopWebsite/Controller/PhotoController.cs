using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class PhotoController
    {
        DatabaseTier.Photo photoDatabase = new DatabaseTier.Photo();
        public PhotoController()
        {

        }

        public string getUserPhotoIDs(string userID)
        {
            string result = photoDatabase.getPhotosUser(userID);
            System.Windows.Forms.MessageBox.Show(result);
            //return photoDatabase.getPhotosUser(userID);
            return result;
        }

        public List<DatabaseTier.Photo> photos (string photoID)
        {
            return null;
        }
    }
}