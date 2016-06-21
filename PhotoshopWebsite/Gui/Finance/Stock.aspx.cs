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
using System.Text.RegularExpressions;

namespace PhotoshopWebsite.Gui
{
    [ExcludeFromCodeCoverage]
    public partial class Stock : System.Web.UI.Page
    {

        private List<Product> products = new List<Product>();
        private ProductController pc;
        private Dictionary<int, int> productAmount;
        private Dictionary<int, int> productStock;


        protected void Page_Load(object sender, EventArgs e)
        {
            pc = new ProductController();
            products = pc.products;
            productStock = new Dictionary<int, int>();
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
            IDHeader.Text = Resources.LocalizedText.product_id;
            TableHeaderCell imageHeader = new TableHeaderCell();
            imageHeader.Text = Resources.LocalizedText.image;
            TableHeaderCell descriptionHeader = new TableHeaderCell();
            descriptionHeader.Text = Resources.LocalizedText.product_description;
            TableHeaderCell stockHeader = new TableHeaderCell();
            stockHeader.Text = Resources.LocalizedText.stock;
            TableHeaderCell amountHeader = new TableHeaderCell();
            amountHeader.Text = Resources.LocalizedText.alter_amount;

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
                productStock.Add(prod.ID, prod.Stock);
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
            if (Session["NumericStock"] != null)
            {
                bool NumericStock = (bool)Session["NumericStock"];
                if (!NumericStock)
                {
                    Response.Write("<script>alert('Vul alleen een numerieke waarde in.')</script>");
                    Session["NumericStock"] = true;
                }
            }
            
            if (Session["PositiveStock"] != null)
            {
                bool PositiveStock = (bool)Session["PositiveStock"];
                if (!PositiveStock)
                {
                    Response.Write("<script>alert('Stock mag niet onder de 0 komen.')</script>");
                    Session["PositiveStock"] = true;
                }
            }
            
        }

        private void AmountText_TextChanged(object sender, EventArgs e)
        {
            TextBox tbAmount = sender as TextBox;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>\\-?[0-9]+)");
            Match match = regex.Match(tbAmount.Text);
            if (match.Success)
            {
                productAmount.Add(int.Parse(tbAmount.ID), int.Parse(tbAmount.Text));
            }
            else
            {
                Session["NumericStock"] = false;
                //Response.Write("<script>alertx('Vul numerice waarden in.')</script>");
            }
        }

        private void AddAmount_Clicked(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, int> item in productAmount)
            {
                foreach (KeyValuePair<int, int> prod in productStock)
                {
                    if (productAmount.ContainsKey(prod.Key))
                    {
                        if (item.Value != 0)
                        {
                            if (prod.Key == item.Key && prod.Value + item.Value >= 0)
                            {
                                pc.updateProductStock(item.Key, item.Value);
                            }
                            else
                            {
                                Session["PositiveStock"] = false;
                            }
                        }
                    }
                }

            }
            Response.Redirect(Request.RawUrl);
        }
    }
}