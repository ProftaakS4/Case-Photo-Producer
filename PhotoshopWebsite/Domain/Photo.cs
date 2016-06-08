using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoshopWebsite.Enumeration;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Photo class is create to create images that can be ordered
    /// </summary>
    [Serializable]
    public class Photo
    {
        DatabaseTier.Photo photo = new DatabaseTier.Photo();

        public int ID { get; set; }
        public int PhotographerID { get; set; }
        public int MapID { get; set; }
        public string Image { get; set; }
        public string Resolution { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// create a user with all possible variables
        /// </summary>
        /// <param name="id">the id of the photo</param>
        /// <param name="photographerID">the id of the photographer to whom the photo belongs to</param>
        /// <param name="map_ID">the map id of the photo</param>
        /// <param name="image">the image path of the photo</param>
        /// <param name="resolution">the resolution of the photo</param>
        /// <param name="description">the description of the photo</param>
        public Photo(int id, int photographerID, int map_ID, string image, string resolution, string description)
        {
            this.ID = id;
            this.PhotographerID = photographerID;
            this.MapID = map_ID;
            this.Image = image;
            this.Resolution = resolution;
            this.Description = description;
        }

        /// <summary>
        /// returns the type of a photo searched for by ID
        /// </summary>
        /// <param name="photoID">the id of the photo</param>
        /// <returns></returns>
        public List<ProductTypes.PTypes> getTypes(string photoID)
        {
            return photo.getTypes(photoID);
        }
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to Point return false.
            Photo p = obj as Photo;
            if ((System.Object)p == null)
            {
                return false;
            }
            // Return true if the fields match:
            return (this.ID == p.ID);
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}