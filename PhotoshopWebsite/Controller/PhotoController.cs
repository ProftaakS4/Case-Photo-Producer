using System;
using System.Collections.Generic;
using System.Data;
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
            return result;
        }
        /// <summary>
        /// get all the photoID's from the USERID
        /// </summary>
        /// <param name="userID"></param> the userID of the account
        /// <returns></returns>
        public List<string> getGroupPhotos()
        {
            List<string> result = new List<string>();
            DataTable dt = photoDatabase.getGroupPhotos();
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    result.Add(data[0].ToString());
                }
            }
            return result;
        }



        /// <summary>
        /// get the photoid from the database and create a photo object based on the stored information
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public Domain.Photo getPhoto(string photoID)
        {
            List<string> photoElements = photoDatabase.getPhoto(photoID);
            if (photoElements.ElementAt(1) != "")
            {
                Domain.Photo photo = new Domain.Photo(Convert.ToInt32(photoElements.ElementAt(0)), Convert.ToInt32(photoElements.ElementAt(1)), Convert.ToInt32(photoElements.ElementAt(2)), photoElements.ElementAt(3), photoElements.ElementAt(4), photoElements.ElementAt(5));
                return photo;
            }
            return null;
        }
    }
}