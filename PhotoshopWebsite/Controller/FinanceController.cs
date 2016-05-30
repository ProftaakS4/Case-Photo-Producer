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
        private DatabaseTier.Finance DB = new DatabaseTier.Finance();

        public int ID { get; set; }
        public List<Finance> finances { get; set; }
        
        public FinanceController()
        {
            this.finances = this.getAllFinances();
        }

        /// <summary>
        /// method to get all the photographers with their first and  last name and the money the company owes them.
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Finance> getAllFinances()
        {
            List<Finance> temp = new List<Finance>();
            DataTable dt = DB.getAllFinances();
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                DataRow[] datarowcategorie = dt.Select("ID=ID");
                foreach (DataRow data in datarowcategorie)
                {
                    temp.Add(new Finance(int.Parse(data[0].ToString()), data[1].ToString(), data[2].ToString(), int.Parse(data[3].ToString())));
                }
            }
            return temp;
        }
    }
}