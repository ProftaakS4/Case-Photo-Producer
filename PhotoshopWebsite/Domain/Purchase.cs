using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class Purchase
    {
        public int ID { get; set; }
        public int accountID { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }

        public Purchase(int id, int accountID, DateTime date, string status)
        {
            this.ID = id;
            this.accountID = accountID;
            this.Date = date;
            this.Status = status;
        }
    }
}