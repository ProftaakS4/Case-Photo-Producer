using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    public class Photo
    {
        public string ID { get; set; }
        public string AccountID { get; set; }
        public string MapID { get; set; }
        public string LoginID { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Resolution { get; set; }
        public string Description { get; set; }

        public Photo(string id, string account_ID, string map_ID, string login_ID, string type, string path, string resolution, string description)
        {
            this.ID = id;
            this.AccountID = account_ID;
            this.MapID = map_ID;
            this.LoginID = login_ID;
            this.Type = type;
            this.Path = path;
            this.Resolution = resolution;
            this.Description = description;
        }
    }
}