using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Finance class is created to keep track of how much money is owed to a photographer
    /// </summary>
    public class Finance
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Money { get; set; }

        /// <summary>
        /// create a Finance with all possible variables
        /// </summary>
        /// <param name="id">if of the photographer</param>
        /// <param name="firstName">firstname of the photographer</param>
        /// <param name="lastName">lastname of the photographer</param>
        /// <param name="money">how much money someone owes a photographer</param>
        public Finance(int id, string firstName, string lastName, int money)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Money = money;
        }
    }
}