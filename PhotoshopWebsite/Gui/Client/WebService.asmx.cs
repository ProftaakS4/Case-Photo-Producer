using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using PhotoshopWebsite.Domain;
using System.Web.Script.Services;

namespace PhotoshopWebsite.Gui.Client.Payment
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod (EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String getUserData()
        {

            if (Session["UserData"] != null)
            {
                User user = Session["UserData"] as User;
                return new JavaScriptSerializer().Serialize(user);
            }
            else
            {
                return null;
            }
         
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String getOrderData()
        {
            if (Session["shoppingCart"] != null)
            {
                List<Domain.ShoppingbasketItem> items = Session["shoppingCart"] as List<Domain.ShoppingbasketItem>;
                return new JavaScriptSerializer().Serialize(items);
            }
            else
            {
                return null;
            }

        }
    }
}
