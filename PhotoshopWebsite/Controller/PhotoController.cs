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
        public List<int> getUserPhotoIDs(int userID)
        {
            List<int> result = new List<int>();
            DataTable dt = photoDatabase.getPhotosUser(userID);
            foreach (DataRow data in dt.Rows)
            {
                result.Add(int.Parse(data[0].ToString()));
            }
            return result;
        }
        /// <summary>
        /// get all the photoID's from the USERID
        /// </summary>
        /// <param name="userID"></param> the userID of the account
        /// <returns></returns>
        public List<int> getGroupPhotos()
        {
            List<int> result = new List<int>();
            DataTable dt = photoDatabase.getGroupPhotos();
            // when data is found and returned
            foreach (DataRow data in dt.Rows)
            {
                result.Add(int.Parse(data[0].ToString()));
            }
            return result;
        }



        /// <summary>
        /// get the photoid from the database and create a photo object based on the stored information
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public Domain.Photo getPhoto(int photoID)
        {
            DataTable dt = photoDatabase.getPhoto(photoID);
            DataRow row = dt.Rows[0];
            if (row != null)
            {
                Domain.Photo photo = new Domain.Photo(int.Parse(row[0].ToString()), int.Parse(row[1].ToString()), int.Parse(row[2].ToString()),row[3].ToString(),row[4].ToString(),row[5].ToString());
                return photo;
            }
            return null;
        }

        public List<int> getPhotoGrapherPhotoIDs(string userID)
        {
            List<int> result = new List<int>();
            DataTable dt = photoDatabase.getPhotosPhotographer(userID);
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow data in dt.Rows)
                {
                    result.Add(int.Parse(data[0].ToString()));
                }
            }
            return result;
        }
    }
}