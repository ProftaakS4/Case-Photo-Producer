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

        public List<string> getUserPhotoIDs(string userID)
        {
            List<string> result = photoDatabase.getPhotosUser(userID);
            if (result!= null)
            {
                return result;
            }
            return null;
        }

        public Domain.Photo getPhoto(string photoID)
        {
            List<string> photoElements = photoDatabase.getPhoto(photoID);
            Domain.Photo photo = new Domain.Photo(photoElements.ElementAt(0), photoElements.ElementAt(1), photoElements.ElementAt(2), photoElements.ElementAt(3), photoElements.ElementAt(4), photoElements.ElementAt(5), photoElements.ElementAt(6), photoElements.ElementAt(7));
            if(photo != null)
            {
                return photo;
            }
            return null;
        }
    }
}