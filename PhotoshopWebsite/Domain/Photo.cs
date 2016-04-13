using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
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
    }
}