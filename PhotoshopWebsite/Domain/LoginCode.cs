using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class LoginCode
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public int SuperUserID { get; set; }
        public int UserID { get; set; }
        public bool Used { get; set; }
        public DateTime ValidUntil { get; set; }

        public LoginCode(int id, int mapID, int superUserID, int userID, bool used, DateTime validUntil)
        {
            this.ID = id;
            this.MapID = mapID;
            this.SuperUserID = superUserID;
            this.UserID = userID;
            this.Used = used;
            this.ValidUntil = validUntil;
        }
    }
}