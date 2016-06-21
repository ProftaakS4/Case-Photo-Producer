using PhotoshopWebsite.Domain;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class FinanceController
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public List<Finance> finances { get; set; }

        public FinanceController()
        {
            this.finances = this.GetAllFinances();
        }

        /// <summary>
        /// method to get all the photographers with their first and  last name and the money the company owes them.
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Finance> GetAllFinances()
        {
            List<Finance> temp = new List<Finance>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            DataTable dt = this.database.CallProcedure("getPricePerPhotographer", parameters);
            // when data is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new Finance(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), int.Parse(data[3].ToString())));
            }
            return temp;
        }
    }
}