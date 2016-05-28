using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
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
            productController = new ProductController();
            Products = productController.products;
            if (Session["products"] != null)
            {
                ProductsChecked = Session["products"] as List<ProductPerPhotographer>;
            }
            else
            {
                ProductsChecked = productController.getProductDataPerPhotographer(currentUser.ID);
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
                tbPrice.ID = "tbPrice" + product.ID.ToString();
                ProductPerPhotographer productPerPhotographer = null;
                foreach (ProductPerPhotographer check in ProductsChecked)
                {
                    if (check.Product_ID == product.ID)
                    {
                        productPerPhotographer = check;
                        break;
                    }
                }
                tbPrice.Text = productPerPhotographer.Price.ToString();
                tbPrice.TextChanged += new EventHandler(this.PriceChange);
                tbPrice.AutoPostBack = true;
                tbPrice.MaxLength = 3;
                priceCell.Controls.Add(tbPrice);

                TableCell availableCell = new TableCell();
                CheckBox cbAvailable = new CheckBox();
                cbAvailable.ID = "cbAvailable" + product.ID.ToString();
                cbAvailable.CssClass = "checkbox";
                cbAvailable.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbAvailable.Height = 30;
                cbAvailable.AutoPostBack = true;
                cbAvailable.Checked = productPerPhotographer.Available;
                availableCell.Controls.Add(cbAvailable);

                MainRow.Cells.Add(typeCell);
                MainRow.Cells.Add(descriptionCell);
                MainRow.Cells.Add(priceCell);
                MainRow.Cells.Add(availableCell);

                MainTable.Rows.Add(MainRow);
            }

            Button btSave = new Button();
            btSave.ID = "btSave";
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
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(cbAvailable.ID);

            int num = Int32.Parse(match.Groups["Numeric"].Value);

            foreach (ProductPerPhotographer productPerPhotographer in ProductsChecked)
            {
                if (productPerPhotographer.Product_ID == num)
                {
                    productPerPhotographer.Available = !productPerPhotographer.Available;
                    Session["products"] = ProductsChecked;
                    Response.Redirect(Request.RawUrl);
                    break;
                }
            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            productController.updateProductsPerPhotographer(ProductsChecked);
            Response.Redirect(Request.RawUrl);
        }

        private void PriceChange(object sender, EventArgs e)
        {
            TextBox cbPrice = sender as TextBox;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(cbPrice.ID);

            int num = Int32.Parse(match.Groups["Numeric"].Value);

            foreach (ProductPerPhotographer productPerPhotographer in ProductsChecked)
            {
                if (productPerPhotographer.Product_ID == num)
                {
                    productPerPhotographer.Price = int.Parse(cbPrice.Text);
                    Session["products"] = ProductsChecked;
                    Response.Redirect(Request.RawUrl);
                    break;
                }
            }
        }
    }
}