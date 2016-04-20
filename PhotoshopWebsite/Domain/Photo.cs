using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoshopWebsite.DatabaseTier;

namespace PhotoshopWebsite.Domain
{
    [Serializable]
    public class Photo
    {
        public int ID { get; set; }
        public int PhotographerID { get; set; }
        public int MapID { get; set; }
        public string Image { get; set; }
        public string Resolution { get; set; }
        public string Description { get; set; }

        public Photo(int id, int photographerID, int map_ID , string image, string resolution, string description)
        {
            this.ID = id;
            this.PhotographerID = photographerID;
            this.MapID = map_ID;
            this.Image = image;
            this.Resolution = resolution;
            this.Description = description;
        }
        public List<ETypes> getTypes(string photoID)
        {
            DatabaseTier.Photo photo = new DatabaseTier.Photo();
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