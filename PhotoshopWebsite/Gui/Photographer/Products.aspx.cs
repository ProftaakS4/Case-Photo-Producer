using PhotoshopWebsite.Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Photographer
{
    [ExcludeFromCodeCoverage]
    public partial class Selection : System.Web.UI.Page
    {
        private List<Product> Products = new List<Product>();
        private ProductController productController;
        private List<int> ProductsChecked;
        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = (User)Session["UserData"];
            productController = new ProductController(currentUser.ID);
            Products = productController.Products;
            if (Session["products"] != null)
            {
                ProductsChecked = Session["products"] as List<int>;
            }
            else
            {
                ProductsChecked = new List<int>();
            }
            Fillpage(this.Products);
        }
        private void Fillpage(List<Product> Products)
        {
            HtmlGenericControl firstcontrol = new HtmlGenericControl();
            firstcontrol.InnerHtml = "<div class='table-responsive'>";
            HtmlGenericControl closingcontrol = new HtmlGenericControl();
            closingcontrol.InnerHtml = "</div>";
            Table MainTable = new Table();
            MainTable.CssClass = "table table-striped table-hover table-bordered";
            MainTable.Width = 600;
            TableHeaderRow MainHeaderRow = new TableHeaderRow();

            TableHeaderCell typeHeader = new TableHeaderCell();
            typeHeader.Text = "Product type";
            TableHeaderCell descriptionHeader = new TableHeaderCell();
            descriptionHeader.Text = "Description";
            TableHeaderCell priceHeader = new TableHeaderCell();
            descriptionHeader.Text = "Price";
            TableHeaderCell availabilityHeader = new TableHeaderCell();
            availabilityHeader.Text = "Availability";

            MainHeaderRow.Cells.Add(typeHeader);
            MainHeaderRow.Cells.Add(descriptionHeader);
            MainHeaderRow.Cells.Add(availabilityHeader);

            MainTable.Rows.Add(MainHeaderRow);

            foreach (Product product in Products)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = product.ID.ToString();
                TableCell User = new TableCell();
                User.Text = product.Used.ToString();
                TableCell ButtonCell = new TableCell();
                CheckBox cbAdd = new CheckBox();
                cbAdd.ID = product.ID.ToString();
                cbAdd.CssClass = "checkbox";
                cbAdd.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbAdd.Height = 30;
                cbAdd.AutoPostBack = true;
                cbAdd.Checked = ProductsChecked.Contains(product.ID);
                ButtonCell.Controls.Add(cbAdd);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(User);
                MainRow.Cells.Add(ButtonCell);

                MainTable.Rows.Add(MainRow);
            }

            Button btSave = new Button();
            btSave.ID = "bt1";
            btSave.Text = "Send Mail";
            btSave.Click += new EventHandler(this.Save_Clicked);
            btSave.Height = 30;

            pnlProducts.Controls.Add(firstcontrol);
            pnlProducts.Controls.Add(MainTable);
            pnlProducts.Controls.Add(closingcontrol);
            pnlProducts.Controls.Add(btSave);
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}