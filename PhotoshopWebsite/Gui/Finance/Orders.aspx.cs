using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;

namespace PhotoshopWebsite.Gui.Finance
{
    [ExcludeFromCodeCoverage]
    public partial class Orders : System.Web.UI.Page
    {
        private List<Purchase> purchases = new List<Purchase>();
        private PurchaseController pc;
        private List<int> purchasesChecked; // To see if the purchases are either Paid or Not Paid.

        protected void Page_Load(object sender, EventArgs e)
        {
            pc = new PurchaseController();
            purchases = pc.purchases;
            if (Session["Allpurchases"] != null)
            {
                purchasesChecked = Session["Allpurchases"] as List<int>;
            }
            else
            {
                purchasesChecked = new List<int>();
            }
            Fillpage(purchases);
        }

        private void Fillpage(List<Purchase> purchases)
        {
            HtmlGenericControl firstcontrol = new HtmlGenericControl();
            firstcontrol.InnerHtml = "<div class='table-responsive'>";
            HtmlGenericControl closingcontrol = new HtmlGenericControl();
            closingcontrol.InnerHtml = "</div>";
            Table MainTable = new Table();
            MainTable.CssClass = "table table-striped table-hover table-bordered";
            MainTable.Width = 600;
            TableHeaderRow MainHeaderRow = new TableHeaderRow();
        
            TableHeaderCell IDHeader = new TableHeaderCell();
            IDHeader.Text = "Purchase ID";
            TableHeaderCell userHeader = new TableHeaderCell();
            userHeader.Text = "Account ID";
            TableHeaderCell dateHeader = new TableHeaderCell();
            dateHeader.Text = "Date";
            TableHeaderCell checkheader = new TableHeaderCell();
            checkheader.Text = "Status";
        
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(userHeader);
            MainHeaderRow.Cells.Add(dateHeader);
            MainHeaderRow.Cells.Add(checkheader);
        
            MainTable.Rows.Add(MainHeaderRow);


            foreach (Purchase pur in purchases)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = pur.ID.ToString();
                TableCell User = new TableCell();
                User.Text = pur.accountID.ToString();
                TableCell Date = new TableCell();
                Date.Text = pur.Date.ToString();
                TableCell ButtonCell = new TableCell();
                //ButtonCell.Text = pur.Status.ToString();
                Label buttonCellText = new Label();
                buttonCellText.Text = pur.Status.ToString();
                ButtonCell.Controls.Add(buttonCellText);

                Literal ltbr = new Literal();
                ltbr.Text = "<BR>";
                ButtonCell.Controls.Add(ltbr);

                if(buttonCellText.Text == "Not paid")
                {
                    CheckBox cbAdd = new CheckBox();
                    cbAdd.ID = pur.ID.ToString();
                    cbAdd.CssClass = "checkbox";
                    cbAdd.CheckedChanged += new EventHandler(this.Check_Clicked);
                    cbAdd.Height = 30;
                    cbAdd.AutoPostBack = true;
                    cbAdd.Checked = purchasesChecked.Contains(pur.ID);
                    ButtonCell.Controls.Add(cbAdd);
                }
                    
                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(User);
                MainRow.Cells.Add(Date);
                MainRow.Cells.Add(ButtonCell);

                MainTable.Rows.Add(MainRow);
            }

            Button btPay = new Button();
            btPay.ID = "bt1";
            btPay.Text = "Pay orders";
            btPay.Click += new EventHandler(this.PayOrder_Clicked);
            btPay.Height = 30;


            pnlCodes.Controls.Add(firstcontrol);
            pnlCodes.Controls.Add(MainTable);
            pnlCodes.Controls.Add(closingcontrol);
            pnlCodes.Controls.Add(btPay);
            //Response.Write("<script>alert('Wrong emailaddress or password')</script>");
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            CheckBox cbAdd = sender as CheckBox;
            foreach (Purchase pur in purchases)
            {
                if (pur.ID.ToString() == cbAdd.ID)
                {
                    if (purchasesChecked.Contains(pur.ID))
                    {
                        purchasesChecked.Remove(pur.ID);
                        Session["Allpurchases"] = purchasesChecked;
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        purchasesChecked.Add(pur.ID);
                        Session["Allpurchases"] = purchasesChecked;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }

        private void PayOrder_Clicked(object sender, EventArgs e)
        {

            foreach (int pur in purchasesChecked)
            {
                pc.updatePurchaseStatus(pur, "Paid");
            }
            Response.Redirect(Request.RawUrl);
        }
        
    }
}