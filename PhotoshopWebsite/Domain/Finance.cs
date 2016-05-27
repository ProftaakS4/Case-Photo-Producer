using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    public class Finance
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Money { get; set; }

        public Finance(int id, string firstName, string lastName, int money)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Money = money;
        }
    }
}