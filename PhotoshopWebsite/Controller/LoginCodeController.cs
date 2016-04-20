using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class LoginCodeController
    {
        private DatabaseTier.LoginCode DB = new DatabaseTier.LoginCode();

        public int ID { get; set; }
        public List<LoginCode> loginCodes { get; set; }
        public LoginCodeController(int photographerID)
        {
            this.ID = photographerID;
            this.loginCodes = this.getLoginCodeData(ID);
        }

        /// <summary>
        /// get loginCodedata of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<LoginCode> getLoginCodeData(int photographerID)
        {
            List<LoginCode> temp = new List<LoginCode>();
            DataTable dt = DB.getLoginCodeData(photographerID);
            // when userdata is found and returned
            if (dt.Rows.Count != 0)
            {
                DataRow[] datarowcategorie = dt.Select("ID=ID");
                foreach (DataRow data in datarowcategorie)
                {
                    temp.Add(new LoginCode(int.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()), true, new DateTime()));
                }
                return temp;
            }
            return new List<LoginCode>();
        }
    }
}