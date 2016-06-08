using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    /// <summary>
    /// LoginCode class is created for keeping track of logincodes that display photo's from the specific photographer
    /// </summary>
    public class LoginCode
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public int SuperUserID { get; set; }
        public int Used { get; set; }
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// create a LoginCode with all possible variables
        /// </summary>
        /// <param name="id">code part of the logincode</param>
        /// <param name="mapID">map that belongs to the logincode</param>
        /// <param name="superUserID">photographer that belongs to the map</param>
        /// <param name="used">times used</param>
        /// <param name="validUntil">experation date</param>
        public LoginCode(int id, int mapID, int superUserID, int used, DateTime validUntil)
        {
            this.ID = id;
            this.MapID = mapID;
            this.SuperUserID = superUserID;
            this.Used = used;
            this.ValidUntil = validUntil;
        }
    }
}