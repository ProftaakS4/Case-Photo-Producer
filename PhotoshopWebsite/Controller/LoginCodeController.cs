using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class LoginCodeController
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public List<LoginCode> loginCodes { get; set; }
        public Boolean validated { get; set; }

        public LoginCodeController(int photographerID, Boolean singleLogincode)
        {
            if (singleLogincode == true)
            {
                this.ID = photographerID;
                this.validated = loginCodeValidation(photographerID);
            }
        }
        public LoginCodeController(int photographerID)
        {
            this.ID = photographerID;
            this.loginCodes = this.getLoginCodeData(ID);
        }

        public bool loginCodeValidation(int loginCode)
        {
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_logincode", new string[] { "int", loginCode.ToString() });
            DataTable dt = database.CallProcedure("checkLoginCode", parameters);
            return dt.Rows.Count != 0;
        }
        /// <summary>
        /// get loginCodedata of the photographer corresponding to photographerID
        /// </summary>
        /// <param name="photographerID"></param>
        /// <returns></returns>
        public List<LoginCode> getLoginCodeData(int photographerID)
        {
            List<LoginCode> temp = new List<LoginCode>();
            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_photographer_ID", new string[] { "int", photographerID.ToString() });
            DataTable dt = database.CallProcedure("getLoginCodesFromPhotographer", parameters);
            // when userdata is found and returned
            foreach (DataRow data in dt.Rows)
            {
                temp.Add(new LoginCode(int.Parse(data[0].ToString()), int.Parse(data[1].ToString()), int.Parse(data[2].ToString()), int.Parse(data[3].ToString()), new DateTime()));
            }
            return temp;
        }
    }
}