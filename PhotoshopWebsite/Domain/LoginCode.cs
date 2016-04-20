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
        public bool Used { get; set; }
        public DateTime ValidUntil { get; set; }

        public LoginCode(int id, int mapID, int superUserID, bool used, DateTime validUntil)
        {
            this.ID = id;
            this.MapID = mapID;
            this.SuperUserID = superUserID;
            this.Used = used;
            this.ValidUntil = validUntil;
        }
    }
}