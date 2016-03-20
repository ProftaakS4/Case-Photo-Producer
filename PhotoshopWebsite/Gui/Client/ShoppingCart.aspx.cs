using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Web.UI.HtmlControls;
using System.Windows.Media;

namespace PhotoshopWebsite.Gui
{
    public partial class ShoppingCart : System.Web.UI.Page
    {        
        private List<Product> shoppingCart;
        protected void Page_Load(object sender, EventArgs e)
        {          
                if (Session["shoppingCart"] != null)
                {
                    shoppingCart = Session["shoppingCart"] as List<Product>;
                    Fillpage(shoppingCart);
                }            
        }
        private void Fillpage(List<Product> productList)
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
            TableHeaderCell TypeHeader = new TableHeaderCell();
            TypeHeader.Text = "Product Type";
            TableHeaderCell MaterialHeader = new TableHeaderCell();
            MaterialHeader.Text = "Product Material";
            TableHeaderCell Descriptionheader = new TableHeaderCell();
            Descriptionheader.Text = "Product Description";
            TableHeaderCell Removeheader = new TableHeaderCell();
            Removeheader.Text = "Remove";
            MainHeaderRow.Cells.Add(IDHeader);
            MainHeaderRow.Cells.Add(TypeHeader);
            MainHeaderRow.Cells.Add(MaterialHeader);
            MainHeaderRow.Cells.Add(Descriptionheader);
            MainHeaderRow.Cells.Add(Removeheader);
            MainTable.Rows.Add(MainHeaderRow);

            foreach (Product product in productList)
            {                                  
                TableRow MainRow = new TableRow();
                MainRow.Height = 80;
                TableCell ID = new TableCell();
                ID.Text = product.ID.ToString();
                TableCell Type = new TableCell();
                Type.Text = product.Type;
                TableCell Material = new TableCell();
                Material.Text = product.Material;
                TableCell Description = new TableCell();
                Description.Text = product.Description;
                
                MainRow.Cells.Add(ID);
                MainRow.Cells.Add(Type);
                MainRow.Cells.Add(Material);
                MainRow.Cells.Add(Description);


                TableCell ButtonCell = new TableCell();
                CheckBox cbRemove = new CheckBox();
                cbRemove.ID = product.ID.ToString();
                cbRemove.CssClass = "checkbox";
                cbRemove.CheckedChanged += new EventHandler(this.Check_Clicked);
                cbRemove.Height = 30;
                cbRemove.AutoPostBack = true;
                cbRemove.Checked = false;

               

                ButtonCell.Controls.Add(cbRemove);
                MainRow.Cells.Add(ButtonCell);
                MainTable.Rows.Add(MainRow);
            }
            pnlProduct.Controls.Add(firstcontrol);
            pnlProduct.Controls.Add(MainTable);
            pnlProduct.Controls.Add(closingcontrol);
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
           
                CheckBox cbremove = sender as CheckBox;
                shoppingCart.ForEach(item =>
                {
                    if (item.ID.ToString() == cbremove.ID)
                    {
                        shoppingCart.Remove(item);
                        Session["shoppingCart"] = shoppingCart;
                        Response.Redirect("ShoppingCart.aspx");
                    }
                }     
            );
        }


    }
}