using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class PurchaseController
    {
        private DatabaseTier.Order DB = new DatabaseTier.Order();

        public int ID { get; set; }
        public List<Purchase> purchases { get; set; }
        
        public PurchaseController()
        {
            this.purchases = this.getAllPurchases();
        }
        
        public PurchaseController(int accountID)
        {
            this.ID = accountID;
            //this.purchases = this.getPurchases(ID);
        }

        /// <summary>
        /// get loginCodedata of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<Purchase> getAllPurchases()
        {
            List<Purchase> temp = new List<Purchase>();
            DataTable dt = DB.getAllPurchases();
            // when data is found and returned
            if (dt.Rows.Count != 0)
            {
                DataRow[] datarowcategorie = dt.Select("ID=ID");
                foreach (DataRow data in datarowcategorie)
                {
                    temp.Add(new Purchase(int.Parse(data[0].ToString()), int.Parse(data[1].ToString()), DateTime.Parse(data[2].ToString()), data[3].ToString()));
                }
            }
            return temp;
        }

        public void updatePurchaseStatus(int purchaseID, string status)
        {
            DB.updatePurchaseStatus(purchaseID, status);
        }
    }
}