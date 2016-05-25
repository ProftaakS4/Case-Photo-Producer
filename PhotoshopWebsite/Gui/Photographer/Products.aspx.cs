using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
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
        private List<ProductPerPhotographer> ProductsChecked;
        protected void Page_Load(object sender, EventArgs e)
        {
            User currentUser = (User)Session["UserData"];
            productController = new ProductController(currentUser.ID);
            Products = productController.products;
            if (Session["products"] != null)
            {
                ProductsChecked = Session["products"] as List<ProductPerPhotographer>;
            }
            else
            {
                ProductsChecked = new List<ProductPerPhotographer>();
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
            TableHeaderCell availableHeader = new TableHeaderCell();
            availableHeader.Text = "Available";

            MainHeaderRow.Cells.Add(typeHeader);
            MainHeaderRow.Cells.Add(descriptionHeader);
            MainHeaderRow.Cells.Add(priceHeader);
            MainHeaderRow.Cells.Add(availableHeader);

            MainTable.Rows.Add(MainHeaderRow);

            foreach (Product product in Products)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 60;
                TableCell typeCell = new TableCell();
                typeCell.Text = product.Type;
                TableCell descriptionCell = new TableCell();
                descriptionCell.Text = product.Description;
                TableCell priceCell = new TableCell();
                //textbox
                TextBox tbPrice = new TextBox();
                tbPrice.ID = product.ID.ToString();
                tbPrice.Text = product.ID.ToString();//get price per photographer
                tbPrice.TextChanged += new EventHandler(this.PriceChange); // pricechange edit
                tbPrice.AutoPostBack = true;
                tbPrice.MaxLength = 3;
                priceCell.Controls.Add(tbPrice);

                TableCell availableCell = new TableCell();
                CheckBox cbAvailable = new CheckBox();
                cbAvailable.ID = product.ID.ToString();
                cbAvailable.CssClass = "checkbox";
                cbAvailable.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbAvailable.Height = 30;
                cbAvailable.AutoPostBack = true;
                ProductPerPhotographer productPerPhotographer = null;
                foreach (ProductPerPhotographer check in ProductsChecked)
                {
                    if (check.Product_ID == product.ID)
                    {
                        productPerPhotographer = check;
                        break;
                    }
                }
                cbAvailable.Checked = productPerPhotographer.Available;
                availableCell.Controls.Add(cbAvailable);

                MainRow.Cells.Add(typeCell);
                MainRow.Cells.Add(descriptionCell);
                MainRow.Cells.Add(priceCell);
                MainRow.Cells.Add(availableCell);

                MainTable.Rows.Add(MainRow);
            }

            Button btSave = new Button();
            btSave.ID = "bt1";
            btSave.Text = "Save";
            btSave.Click += new EventHandler(this.Save_Clicked);
            btSave.Height = 30;

            pnlProducts.Controls.Add(firstcontrol);
            pnlProducts.Controls.Add(MainTable);
            pnlProducts.Controls.Add(closingcontrol);
            pnlProducts.Controls.Add(btSave);
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            CheckBox cbAvailable = sender as CheckBox;
            foreach (ProductPerPhotographer productPerPhotographer in ProductsChecked)
            {
                if (productPerPhotographer.Product_ID.ToString() == cbAvailable.ID)
                {
                    productPerPhotographer.Available = !productPerPhotographer.Available;
                    Session["products"] = ProductsChecked;
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {

            throw new NotImplementedException();
        }

        private void PriceChange(object sender, EventArgs e)
        {

            throw new NotImplementedException();
        }
    }
}