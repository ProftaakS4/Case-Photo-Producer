using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
using System.Web.UI.HtmlControls;
using System.Diagnostics.CodeAnalysis;

namespace PhotoshopWebsite.Gui.Photographer
{
    [ExcludeFromCodeCoverage]
    public partial class Clients : System.Web.UI.Page
    {
        private List<LoginCode> loginCodes = new List<LoginCode>();
        private LoginCodeController lcc;
        private List<int> loginCodesChecked;
        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = (User)Session["UserData"];
            lcc = new LoginCodeController(currentUser.ID);
            loginCodes = lcc.loginCodes;
            if (Session["loginCodes"] != null)
            {
                loginCodesChecked = Session["loginCodes"] as List<int>;
            }
            else
            {
                loginCodesChecked = new List<int>();
            }
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
            IDHeader.Text = Resources.LocalizedText.order_code;
            TableHeaderCell userHeader = new TableHeaderCell();
            userHeader.Text = Resources.LocalizedText.times_used;
            TableHeaderCell checkheader = new TableHeaderCell();
            checkheader.Text = Resources.LocalizedText.send;

            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(userHeader);
            MainHeaderRow.Cells.Add(checkheader);

            MainTable.Rows.Add(MainHeaderRow);

            foreach (LoginCode code in loginCodes)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 60;
                TableCell ID = new TableCell();
                ID.Text = code.ID.ToString();
                TableCell User = new TableCell();
                User.Text = code.Used.ToString();
                TableCell ButtonCell = new TableCell();
                CheckBox cbAdd = new CheckBox();
                cbAdd.ID = code.ID.ToString();
                cbAdd.CssClass = "checkbox";
                cbAdd.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbAdd.Height = 30;
                cbAdd.AutoPostBack = true;
                cbAdd.Checked = loginCodesChecked.Contains(code.ID);
                ButtonCell.Controls.Add(cbAdd);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(User);
                MainRow.Cells.Add(ButtonCell);

                MainTable.Rows.Add(MainRow);
            }
            TextBox tbMailTo = new TextBox();
            tbMailTo.ID = "TextBoxRow_mailto1";
            tbMailTo.Text = Resources.LocalizedText.email;
            if (Session["mailTO"] as String != null)
            {
                tbMailTo.Text = Session["mailTO"] as String;
            }
            tbMailTo.TextChanged += new EventHandler(this.tbMailTo_Change);
            tbMailTo.CssClass = "form-control";
            tbMailTo.Width = 250;
            tbMailTo.Height = 30;
            tbMailTo.AutoPostBack = true;

            Button btSend = new Button();
            btSend.ID = "bt1";
            btSend.Text = Resources.LocalizedText.send;
            btSend.Click += new EventHandler(this.Mail_Clicked);
            btSend.Height = 30;

            pnlCodes.Controls.Add(firstcontrol);
            pnlCodes.Controls.Add(MainTable);
            pnlCodes.Controls.Add(closingcontrol);
            pnlCodes.Controls.Add(tbMailTo);
            pnlCodes.Controls.Add(btSend);
        }
        private void Mail_Clicked(object sender, EventArgs e)
        {
            Button btSend = sender as Button;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                // Mail Header
                mail.From = new MailAddress("photoshopPTS4@gmail.com");
                mail.To.Add(Session["mailTO"] as String);
                mail.Subject = Resources.LocalizedText.your_order_codes;
                // Mail Body
                StringBuilder sb = new StringBuilder();
                sb.Append(Resources.LocalizedText.your_codes_are + Environment.NewLine);
                foreach (int code in loginCodesChecked)
                {
                    sb.Append(code + Environment.NewLine);
                }
                sb.Append(Resources.LocalizedText.your_codes_description);
                mail.Body = sb.ToString();
                // Mail Config
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("photoshopPTS4@gmail.com", "proftaak4");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                Response.Write("<script>alert('"+Resources.LocalizedText.mail_sent+" " + Session["mailTO"] as String + "')</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+Resources.LocalizedText.error_cant_send_mail+" " + Session["mailTO"] as String + "')</script>");
            }

        }
        private void tbMailTo_Change(object sender, EventArgs e)
        {
            TextBox tbMailTo = sender as TextBox;
            Session["mailTO"] = tbMailTo.Text;
            Response.Redirect(Request.RawUrl);
        }
        private void Check_Clicked(object sender, EventArgs e)
        {
            CheckBox cbAdd = sender as CheckBox;
            foreach (LoginCode code in loginCodes)
            {
                if (code.ID.ToString() == cbAdd.ID)
                {
                    if (loginCodesChecked.Contains(code.ID))
                    {
                        loginCodesChecked.Remove(code.ID);
                        Session["loginCodes"] = loginCodesChecked;
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        loginCodesChecked.Add(code.ID);
                        Session["loginCodes"] = loginCodesChecked;
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }
    }
}