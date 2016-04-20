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

        /// <summary>
        /// get all the photoID's from the USERID
        /// </summary>
        /// <param name="userID"></param> the userID of the account
        /// <returns></returns>
        public List<string> getUserPhotoIDs(string userID)
        {
            List<string> result = photoDatabase.getPhotosUser(userID);
            if (result!= null)
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// get the photoid from the database and create a photo object based on the stored information
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public Domain.Photo getPhoto(string photoID)
        {
            List<string> photoElements = photoDatabase.getPhoto(photoID);
            Domain.Photo photo = new Domain.Photo(Convert.ToInt32(photoElements.ElementAt(0)), Convert.ToInt32(photoElements.ElementAt(1)), Convert.ToInt32(photoElements.ElementAt(2)), photoElements.ElementAt(3),photoElements.ElementAt(4), photoElements.ElementAt(5));
            if(photo != null)
            {
                return photo;
            }
            return null;
        }
    }
}