using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;

namespace PhotoshopWebsite.Gui.Photographer
{
    public partial class Clients : System.Web.UI.Page
    {
        List<LoginCode> loginCodes;
        LoginCodeController lcc;
        string mailTO = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lcc = new LoginCodeController(1);
            loginCodes = lcc.loginCodes;
            Fillpage(this.loginCodes);
        }
        private void Fillpage(List<LoginCode> loginCodes)
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
            IDHeader.Text = "Code ID";
            TableHeaderCell userHeader = new TableHeaderCell();
            userHeader.Text = "Account ID user";
            TableHeaderCell mailToHeader = new TableHeaderCell();
            mailToHeader.Text = "MailTo";
            TableHeaderCell sendHeader = new TableHeaderCell();
            sendHeader.Text = "Send";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(userHeader);
            MainHeaderRow.Cells.Add(mailToHeader);
            MainHeaderRow.Cells.Add(sendHeader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (LoginCode code in loginCodes)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = code.ID.ToString();
                TableCell User = new TableCell();
                User.Text = code.UserID.ToString();
                TableCell MailTo = new TableCell();
                TextBox tbMailTo = new TextBox();
                tbMailTo.ID = "TextBoxRow_" + code.ID;
                tbMailTo.Text = "";
                tbMailTo.TextChanged += new EventHandler(this.tbMailTo_Change);
                tbMailTo.AutoPostBack = true;
                MailTo.Controls.Add(tbMailTo);

                TableCell ButtonCell = new TableCell();
                Button btSend = new Button();
                btSend.ID = code.ID.ToString();
                btSend.CssClass = "btn btn-default";
                btSend.Click += new EventHandler(this.Mail_Clicked);
                btSend.Height = 30;
                ButtonCell.Controls.Add(btSend);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(User);
                MainRow.Cells.Add(MailTo);
                MainRow.Cells.Add(ButtonCell);

                MainTable.Rows.Add(MainRow);
            }


            pnlCodes.Controls.Add(firstcontrol);
            pnlCodes.Controls.Add(MainTable);
            pnlCodes.Controls.Add(closingcontrol);
        }
        private void Mail_Clicked(object sender, EventArgs e)
        {
            Button btSend = sender as Button;
            LoginCode lc = null;
            //mail
            foreach (LoginCode code in loginCodes)
            {
                if (code.ID == int.Parse(btSend.ID))
                {
                    lc = code;
                    break;
                }
            }
            if (lc != null)
            {
                Response.Write("<script>alert('mail id number " + lc.ID.ToString() + " to: " + mailTO + "')</script>");
            }
        }
        private void tbMailTo_Change(object sender, EventArgs e)
        {
            TextBox tbMailTo = sender as TextBox;
            mailTO = tbMailTo.Text;
        }
    }
}