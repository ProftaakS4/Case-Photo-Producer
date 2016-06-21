using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Domain;
using System.Web.UI.HtmlControls;
using PhotoshopWebsite.Controller;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class Administration : System.Web.UI.Page
    {

        private List<Domain.Finance> finances = new List<Domain.Finance>();
        private FinanceController fc;

        protected void Page_Load(object sender, EventArgs e)
        {
            fc = new FinanceController();
            finances = fc.finances;
            Fillpage(finances);
        }

        private void Fillpage(List<Domain.Finance> finances)
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
            IDHeader.Text = Resources.LocalizedText.photographer_id;
            TableHeaderCell firstNameHeader = new TableHeaderCell();
            firstNameHeader.Text = Resources.LocalizedText.first_name;
            TableHeaderCell lastNameHeader = new TableHeaderCell();
            lastNameHeader.Text = Resources.LocalizedText.last_name;
            TableHeaderCell moneyHeader = new TableHeaderCell();
            moneyHeader.Text = Resources.LocalizedText.money_owed;

            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(firstNameHeader);
            MainHeaderRow.Cells.Add(lastNameHeader);
            MainHeaderRow.Cells.Add(moneyHeader);

            MainTable.Rows.Add(MainHeaderRow);


            foreach (Domain.Finance fin in finances)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = fin.ID.ToString();
                TableCell firstName = new TableCell();
                firstName.Text = fin.FirstName.ToString();

                TableCell lastName = new TableCell();
                lastName.Text = fin.LastName.ToString();

                TableCell moneyOwed = new TableCell();
                moneyOwed.Text = "€ " + fin.Money.ToString() + ",00";

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(firstName);
                MainRow.Cells.Add(lastName);
                MainRow.Cells.Add(moneyOwed);

                MainTable.Rows.Add(MainRow);
            }

            pnlCodes.Controls.Add(firstcontrol);
            pnlCodes.Controls.Add(MainTable);
            pnlCodes.Controls.Add(closingcontrol);
        }

    }
}