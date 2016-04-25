using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using PhotoshopWebsite.DatabaseTier;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PhotoshopWebsite
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        // create instance of the photoController for future database connections through busisness layer
        PhotoController photoController = new PhotoController();

        // create a list of all the current user photos
        public List<Domain.Photo> photos
        {
            get
            {
                if (!(Session["photos"] is List<Domain.Photo>))
                {
                    Session["photos"] = new List<Domain.Photo>();
                }

                return Session["photos"] as List<Domain.Photo>;
            }
        }

        public List<Domain.Photo> searchedPhotos
        {
            get
            {
                if (!(Session["searchedPhotos"] is List<Domain.Photo>))
                {
                    Session["searchedPhotos"] = new List<Domain.Photo>();
                }

                return Session["searchedPhotos"] as List<Domain.Photo>;
            }
        }

        public List<Domain.ShoppingbasketItem> shoppingCart
        {
            get
            {
                if (!(Session["shoppingCart"] is List<Domain.ShoppingbasketItem>))
                {
                    Session["shoppingCart"] = new List<Domain.ShoppingbasketItem>();
                }

                return Session["shoppingCart"] as List<Domain.ShoppingbasketItem>;
            }
        }

        public Dictionary<int, string> filters
        {
            get
            {
                if (!(Session["filters"] is Dictionary<int, string>))
                {
                    Session["filters"] = new Dictionary<int, string>();
                }

                return Session["filters"] as Dictionary<int, string>;
            }
        }

        public Dictionary<int, ProductTypes.ETypes> products
        {
            get
            {
                if (!(Session["products"] is Dictionary<int, ProductTypes.ETypes>))
                {
                    Session["products"] = new Dictionary<int, ProductTypes.ETypes>();
                }

                return Session["products"] as Dictionary<int, ProductTypes.ETypes>;
            }
        }


        private Bitmap _current;


        protected void Page_Load(object sender, EventArgs e)
        {
            // cast the session into the current user
            User currenUser = (User)Session["UserData"];

            // get the userid of the current user
            string userID = Convert.ToString(currenUser.ID);

            // get all the photoID's of the current user
            List<string> photoIDS = photoController.getUserPhotoIDs(userID);

            // get all the photos of the current user and add them to a list
            if (photoIDS != null && photos.Count == 0)
            {
                foreach (string s in photoIDS)
                {
                    // store all the photos in the session
                    photos.Add(photoController.getPhoto(s));
                }
            }

            // check if a search has taken place
            if (searchedPhotos.Count > 0)
            {
                // fill the page with the users photos
                foreach (Domain.Photo photo in searchedPhotos)
                {
                    Fillpage(photo);
                }
            }
            // if no search then show the normal photos
            else
            {
                // fill the page with the users photos
                foreach (Domain.Photo photo in photos)
                {
                    Fillpage(photo);
                }
            }
        }


        private void Fillpage(Domain.Photo x)
        {
            //create buttons
            Button btnAddToCart = new Button();
            btnAddToCart.ID = x.Description + x.ID.ToString();
            btnAddToCart.CssClass = "btn btn-default";
            btnAddToCart.Click += btnAddToCart_Click;
            btnAddToCart.Height = 30;
            btnAddToCart.Text = "Order";

            RadioButton btnSepia = new RadioButton();
            btnSepia.ID = "btnSepia" + x.ID;
            btnSepia.CheckedChanged += filterChange;
            btnSepia.AutoPostBack = true;
            btnSepia.GroupName = x.ID.ToString();
            btnSepia.Height = 30;
            btnSepia.Text = "Sepia ";

            RadioButton btnBlackWhite = new RadioButton();
            btnBlackWhite.ID = "btnBlackWhite" + x.ID;
            btnBlackWhite.CheckedChanged += filterChange;
            btnBlackWhite.AutoPostBack = true;
            btnBlackWhite.GroupName = x.ID.ToString();
            btnBlackWhite.Height = 30;
            btnBlackWhite.Text = "Black & White ";

            RadioButton btnColor = new RadioButton();
            btnColor.ID = "btnColor" + x.ID;
            btnColor.CheckedChanged += filterChange;
            btnColor.AutoPostBack = true;
            btnColor.GroupName = x.ID.ToString();
            btnColor.Height = 30;
            btnColor.Text = "Color ";
            if (!filters.ContainsKey(x.ID))
            {
                filters.Add(x.ID, "btnColor");
            }
            if (!products.ContainsKey(x.ID))
            {
                products.Add(x.ID, ProductTypes.ETypes.PHOTO1x2);
            }

            switch (filters[x.ID])
            {
                case "btnColor":
                    btnColor.Checked = true;
                    break;
                case "btnBlackWhite":
                    btnColor.Checked = true;
                    break;
                case "btnSepia":
                    btnColor.Checked = true;
                    break;
                default:
                    btnColor.Checked = true;
                    break;
            }
            colorChange(x.ID);

            DropDownList ddType = new DropDownList();
            ddType.ID = "ddType" + x.ID;
            ddType.CssClass = "form-control";
            ddType.Width = 94;
            ddType.Height = 30;
            ddType.SelectedIndexChanged += ddType_SelectedIndexChanged;
            //Gets the product types offered by the photographer per photo
            List<ProductTypes.ETypes> types = x.getTypes(x.ID.ToString());
            foreach (ProductTypes.ETypes type in types)
            {
                ListItem Li = new ListItem();
                Li.Value = type.ToString();
                Li.Text = type.ToString();
                ddType.Items.Add(Li);
            }

            System.Web.UI.WebControls.Image imgProduct = new System.Web.UI.WebControls.Image();
            imgProduct.ID = "image" + x.ID.ToString();
            imgProduct.AlternateText = "No Image found, please the contact administrator";
            imgProduct.ImageUrl = x.Image;
            imgProduct.Height = 200;
            imgProduct.Width = 330;

            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");
            //adding other div elements containing discriptions
            string div;
            if (photos.Count > 4)
            {
                div = "<div class='col-sm-4'>";
            }
            else
            {
                div = "<div class='col-sm-6'>";
            }

            //firstControl.InnerHtml = div + "<div class='thumbnail' style='max-width:330px max-height:150px;'> <img src=" + x.Image + " " + "alt=" + x.Description + ">  <div class='caption'>";
            firstControl.InnerHtml = div + "<div class='thumbnail' style='max-width:330px max-height:150px;'><div class='caption'>";

            //add buttons
            secondControl.InnerHtml = "<p>" + x.Description + "</p>";
            firstControl.Controls.Add(imgProduct);
            firstControl.Controls.Add(secondControl);
            firstControl.Controls.Add(btnColor);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(new LiteralControl("<br />"));
            firstControl.Controls.Add(ddType);
            firstControl.Controls.Add(btnAddToCart);

            pnlProduct.Controls.Add(firstControl);

            lastControl.InnerHtml = "</div></div></div>";
            pnlProduct.Controls.Add(lastControl);
        }

        void ddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(ddl.ID);

            int num = Int32.Parse(match.Groups["Numeric"].Value);
            //set EType on ID
            products[num] = ProductTypes.getEType(ddl.SelectedValue);
        }

        void filterChange(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(button.ID);

            string name = match.Groups["Alpha"].Value;
            int num = Int32.Parse(match.Groups["Numeric"].Value);

            //set name on ID
            filters[num] = name;
            //change color
            colorChange(num);
        }
        void colorChange(int num)
        {
            string name = filters[num];

            foreach (Domain.Photo photo in photos)
            {
                if (photo.ID == num)
                {
                    switch (name)
                    {
                        case "btnColor":
                            convertColor(photo);
                            break;
                        case "btnBlackWhite":
                            convertBlackWhite(photo);
                            break;
                        case "btnSepia":
                            convertSepia(photo);
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(button.ID);
            
            string name = match.Groups["Alpha"].Value;
            int num = Int32.Parse(match.Groups["Numeric"].Value);

            Domain.ShoppingbasketItem found = null;
            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                if (item.photoID == num && item.filterType == filters[num])
                {
                    found = item;
                    break;
                }
            }
            if (found != null)
            {
                found.quantity++;
            }
            else
            {
                //TODO pakt ook de jaartallen niet alleen de ID's
                shoppingCart.Add(new Domain.ShoppingbasketItem(num, name, filters[num], products[num]));
            }
        }

        private void convertColor(Domain.Photo photo)
        {
            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
                        {
                            currentImage.ImageUrl = photo.Image;
                            return;
                        }
                    }
                }
            }
        }

        private void convertSepia(Domain.Photo photo)
        {
            _current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Image.ToString()));
            Bitmap temp = (Bitmap)_current;
            Bitmap bmap = (Bitmap)temp.Clone();

            for (int yCoordinate = 0; yCoordinate < bmap.Height; yCoordinate++)
            {
                for (int xCoordinate = 0; xCoordinate < bmap.Width; xCoordinate++)
                {
                    Color color = bmap.GetPixel(xCoordinate, yCoordinate);
                    double grayColor = ((double)(color.R + color.G + color.B)) / 3.0d;
                    Color sepia = Color.FromArgb((byte)grayColor, (byte)(grayColor * 0.95), (byte)(grayColor * 0.82));
                    bmap.SetPixel(xCoordinate, yCoordinate, sepia);
                }
            }
            _current = (Bitmap)bmap.Clone();
            _current.Save(Server.MapPath("../Images/Sepia" + photo.ID + ".png"));

            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
                        {
                            currentImage.ImageUrl = "../Images/Sepia" + photo.ID + ".png";
                            break;
                        }
                    }
                }
            }
        }

        private void convertBlackWhite(Domain.Photo photo)
        {
            _current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Image.ToString()));
            Bitmap temp = (Bitmap)_current;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color col;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    col = bmap.GetPixel(i, j);
                    byte grey = (byte)(.299 * col.R + .587 * col.G + .114 * col.B);
                    bmap.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            }
            _current = (Bitmap)bmap.Clone();
            Random rnd = new Random();
            int a = rnd.Next();
            _current.Save(Server.MapPath("../Images/BlackWhite" + photo.ID + ".png"));

            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
                        {
                            currentImage.ImageUrl = "../Images/BlackWhite" + photo.ID + ".png";
                            break;
                        }
                    }
                }
            }
        }
    }
}




