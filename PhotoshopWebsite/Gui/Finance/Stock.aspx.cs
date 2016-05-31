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
    public partial class Stock : System.Web.UI.Page
    {

        private List<Product> products = new List<Product>();
        private ProductController pc;
        private Dictionary<int, int> productAmount;


        protected void Page_Load(object sender, EventArgs e)
        {
            pc = new ProductController();
            products = pc.products;
            if (Session["Stockpurchases"] != null)
            {
                productAmount = Session["Stockpurchases"] as Dictionary<int, int>;
            }
            else
            {
                productAmount = new Dictionary<int,int>();
            }
            Fillpage(products);
        }

        private void Fillpage(List<Product> products)
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
            IDHeader.Text = "Product ID";
            TableHeaderCell imageHeader = new TableHeaderCell();
            imageHeader.Text = "Image";
            TableHeaderCell descriptionHeader = new TableHeaderCell();
            descriptionHeader.Text = "Description";
            TableHeaderCell stockHeader = new TableHeaderCell();
            stockHeader.Text = "Stock";
            TableHeaderCell amountHeader = new TableHeaderCell();
            amountHeader.Text = "Alter Amount";

            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(imageHeader);
            MainHeaderRow.Cells.Add(descriptionHeader);
            MainHeaderRow.Cells.Add(stockHeader);
            MainHeaderRow.Cells.Add(amountHeader);

            MainTable.Rows.Add(MainHeaderRow);


            foreach (Product prod in products)
            {
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = prod.ID.ToString();
                TableCell Image = new TableCell();
                System.Web.UI.WebControls.Image imgProduct = new System.Web.UI.WebControls.Image();
                imgProduct.ImageUrl = prod.Image;
                imgProduct.Height = 100;
                imgProduct.Width = 150;
                
                Image.Controls.Add(imgProduct);

                TableCell Description = new TableCell();
                Description.Text = prod.Description.ToString();
                TableCell Stock = new TableCell();
                Stock.Text = prod.Stock.ToString();
                TableCell TextBoxCell = new TableCell();
                Label buttonCellText = new Label();
                buttonCellText.Text = "Amount: ";
                TextBoxCell.Controls.Add(buttonCellText);

                Literal ltbr = new Literal();
                ltbr.Text = "<BR>";
                TextBoxCell.Controls.Add(ltbr);

                TextBox tbAmountStock = new TextBox();
                tbAmountStock.ID = prod.ID.ToString();
                tbAmountStock.Text = "0";
                tbAmountStock.TextChanged += new EventHandler(this.AmountText_TextChanged);
                //tbAmountStock.CssClass = "textbox";
                TextBoxCell.Controls.Add(tbAmountStock);

                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Image);
                MainRow.Cells.Add(Description);
                MainRow.Cells.Add(Stock);
                MainRow.Cells.Add(TextBoxCell);

                MainTable.Rows.Add(MainRow);
            }

            Button btPay = new Button();
            btPay.ID = "bt1";
            btPay.Text = "Alter Amount";
            btPay.Click += new EventHandler(this.AddAmount_Clicked);
            btPay.Height = 30;


            pnlCodes.Controls.Add(firstcontrol);
            pnlCodes.Controls.Add(MainTable);
            pnlCodes.Controls.Add(closingcontrol);
            pnlCodes.Controls.Add(btPay);
            //Response.Write("<script>alert('Wrong emailaddress or password')</script>");
        }

        private void AmountText_TextChanged(object sender, EventArgs e)
        {
            TextBox tbAmount = sender as TextBox;
            productAmount.Add(int.Parse(tbAmount.ID), int.Parse(tbAmount.Text));
        }

        private void AddAmount_Clicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, int> item in productAmount)
            {
                pc.updateProductStock(item.Key, item.Value);
            }
            Response.Redirect(Request.RawUrl);
        }

    }
}