﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Drawing;
using System.Text.RegularExpressions;
using PhotoshopWebsite.Enumeration;
using System.Diagnostics.CodeAnalysis;
using PhotoshopWebsite.Domain;

namespace PhotoshopWebsite
{
    [ExcludeFromCodeCoverage]
    public partial class WebForm1 : System.Web.UI.Page
    {
        // create instance of the photoController for future database connections through busisness layer
        PhotoController photoController = new PhotoController();
        int[] cropValues = new int[6];

        // create a list of all the current user photos
        public List<Domain.Photo> photos;

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

        public Dictionary<int, FilterTypes.FTypes> filters
        {
            get
            {
                if (!(Session["filters"] is Dictionary<int, FilterTypes.FTypes>))
                {
                    Session["filters"] = new Dictionary<int, FilterTypes.FTypes>();
                }

                return Session["filters"] as Dictionary<int, FilterTypes.FTypes>;
            }
        }

        public Dictionary<int, ProductTypes.PTypes> products
        {
            get
            {
                if (!(Session["products"] is Dictionary<int, ProductTypes.PTypes>))
                {
                    Session["products"] = new Dictionary<int, ProductTypes.PTypes>();
                }

                return Session["products"] as Dictionary<int, ProductTypes.PTypes>;
            }
        }

        private Bitmap _current;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["photos"] = null;
            }
            // cast the session into the current user
            User currenUser = (User)Session["UserData"];

            // get the userid of the current user
            int userID = currenUser.ID;

            // get all the photoID's of the current user
            List<int> photoIDS = photoController.getUserPhotoIDs(userID);

            if (Session["photos"] != null)
            {
                photos = (List<Domain.Photo>)Session["photos"];
            }
            else
            {
                photos = new List<Domain.Photo>();

                // get all the photos of the current user and add them to a list
                if (photoIDS != null)
                {
                    foreach (int s in photoIDS)
                    {
                        // store all the photos in the session
                        photos.Add(photoController.getPhoto(s));
                    }
                    Session["photos"] = photos;
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
            else
            {
                // if no search then show the normal photos
                // fill the page with the users photos
                foreach (Domain.Photo photo in photos)
                {
                    Fillpage(photo);
                }
            }
        }

        private void Fillpage(Domain.Photo x)
        {
            // create buttons
            Button btnAddToCart = new Button();
            btnAddToCart.ID = "{" + x.Description + "}" + x.ID.ToString();
            btnAddToCart.CssClass = "btn btn-default";
            btnAddToCart.Click += this.BtnAddToCart_Click;
            btnAddToCart.Height = 30;
            btnAddToCart.Text = Resources.LocalizedText.order_image;

            RadioButton btnSepia = new RadioButton();
            btnSepia.ID = "SEPIA" + x.ID;
            btnSepia.CheckedChanged += FilterChange;
            btnSepia.AutoPostBack = true;
            btnSepia.GroupName = x.ID.ToString();
            btnSepia.Height = 30;
            btnSepia.Text = Resources.LocalizedText.sepia;

            HtmlGenericControl cropButtonControll = new HtmlGenericControl("div");
            cropButtonControll.InnerHtml = "<button type='button' id='Cropbtn" + x.ID + "'class='btn btn-default' data-toggle='modal' data-target='#myModal" + x.ID + "' >" + Resources.LocalizedText.crop + "</ button >";

            RadioButton btnBlackWhite = new RadioButton();
            btnBlackWhite.ID = "BLACKWHITE" + x.ID;
            btnBlackWhite.CheckedChanged += FilterChange;
            btnBlackWhite.AutoPostBack = true;
            btnBlackWhite.GroupName = x.ID.ToString();
            btnBlackWhite.Height = 30;
            btnBlackWhite.Text = Resources.LocalizedText.black_white;

            RadioButton btnColor = new RadioButton();
            btnColor.ID = "COLOR" + x.ID;
            btnColor.CheckedChanged += FilterChange;
            btnColor.AutoPostBack = true;
            btnColor.GroupName = x.ID.ToString();
            btnColor.Height = 30;
            btnColor.Text = Resources.LocalizedText.color;

            Button btnCrop = new Button();
            btnCrop.ID = "Crop" + x.ID.ToString();
            btnCrop.Click += this.BtnCrop_Click;
            btnCrop.CssClass = "btn btn-default";
            btnCrop.Text = Resources.LocalizedText.crop;
            btnCrop.Height = 30;

            if (!filters.ContainsKey(x.ID))
            {
                filters.Add(x.ID, FilterTypes.FTypes.COLOR);
            }
            if (!products.ContainsKey(x.ID))
            {
                products.Add(x.ID, ProductTypes.PTypes.PHOTO1x2);
            }

            switch (filters[x.ID])
            {
                case FilterTypes.FTypes.COLOR:
                    btnColor.Checked = true;
                    break;
                case FilterTypes.FTypes.BLACKWHITE:
                    btnBlackWhite.Checked = true;
                    break;
                case FilterTypes.FTypes.SEPIA:
                    btnSepia.Checked = true;
                    break;
                default:
                    btnColor.Checked = true;
                    break;
            }
            ColorChange(x.ID);

            DropDownList ddType = new DropDownList();
            ddType.ID = "ddType" + x.ID;
            ddType.CssClass = "form-control";
            ddType.Width = 94;
            ddType.Height = 30;
            ddType.SelectedIndexChanged += DDType_SelectedIndexChanged;
            // Gets the product types offered by the photographer per photo
            List<ProductTypes.PTypes> types = x.getTypes(x.ID);
            foreach (ProductTypes.PTypes type in types)
            {
                ListItem Li = new ListItem();
                Li.Value = type.ToString();
                Li.Text = type.ToString();
                ddType.Items.Add(Li);
            }

            System.Web.UI.WebControls.Image imgProduct = new System.Web.UI.WebControls.Image();
            imgProduct.ID = "image" + x.ID.ToString();
            imgProduct.AlternateText = Resources.LocalizedText.error_no_image_found;
            imgProduct.ImageUrl = x.Image;
            imgProduct.Height = 200;
            imgProduct.Width = 330;
            imgProduct.CssClass = "img-responsive img-thumbnail";

            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl cropControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");
            HtmlGenericControl cropControlLast = new HtmlGenericControl("div");

            string div = "<div class='col-sm-4'>";

            firstControl.InnerHtml = div + "<div class='thumbnail' style='max-width:330px max-height:150px;'><div class='caption'>";
            cropControl.InnerHtml = "<div class='modal fade' id='myModal" + x.ID + "' tabindex=' - 1' role='dialog' aria-labelledby='mymodallabel'>< div class='modal-dialog' role='document'><div class='modal-content'  style='width:400px'><div class='modal-header'><button type = 'button' class='close' data-dismiss='modal' aria-label='close'><span aria-hidden='true'>&times;</span></button><h4 class='modal-title' id='mymodallabel'>" + Resources.LocalizedText.order_image + "</h4></div><div class='modal-body'> <img src='" + x.Image + "' class='cropbox' style='height:330px; width:200px;'></img></div><div class='modal-footer'><button type = 'button' class='btn btn-default' data-dismiss='modal'>" + Resources.LocalizedText.close + "</button>";
            cropControl.Controls.Add(btnCrop);
            cropControlLast.InnerHtml = "</div></div</div></div>";

            // add buttons
            secondControl.InnerHtml = "<p>" + x.Description + "</p>";
            firstControl.Controls.Add(imgProduct);
            firstControl.Controls.Add(secondControl);
            firstControl.Controls.Add(btnColor);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(new LiteralControl("<br />"));
            firstControl.Controls.Add(ddType);
            firstControl.Controls.Add(btnAddToCart);

            firstControl.Controls.Add(cropButtonControll);
            pnlProduct.Controls.Add(firstControl);

            pnlProduct.Controls.Add(cropControl);
            pnlProduct.Controls.Add(cropControlLast);

            lastControl.InnerHtml = "</div></div></div>";
            pnlProduct.Controls.Add(lastControl);
        }

        private void BtnCrop_Click(object sender, EventArgs e)
        {
            // bij het aanmaken van de button wordt de photo id gebruikt als button id. 
            Button button = sender as Button;
            // get the fotoid by using the crop button id whicht contains the photoid
            string photoId = button.ID.Replace("Crop", string.Empty);

            if (input_X.Value.Contains("."))
            {
                cropValues[0] = Convert.ToInt32(input_X.Value.Substring(0, input_X.Value.IndexOf(".")));
            }
            else
            {
                cropValues[0] = Convert.ToInt32(input_X.Value);
            }
            if (input_Y.Value.Contains("."))
            {
                cropValues[1] = Convert.ToInt32(input_Y.Value.Substring(0, input_Y.Value.IndexOf(".")));
            }
            else
            {
                cropValues[1] = Convert.ToInt32(input_Y.Value);
            }
            if (input_W.Value.Contains("."))
            {
                cropValues[2] = Convert.ToInt32(input_W.Value.Substring(0, input_W.Value.IndexOf(".")));
            }
            else
            {
                cropValues[2] = Convert.ToInt32(input_W.Value);
            }
            if (input_H.Value.Contains("."))
            {
                cropValues[3] = Convert.ToInt32(input_H.Value.Substring(0, input_H.Value.IndexOf(".")));
            }
            else
            {
                cropValues[3] = Convert.ToInt32(input_H.Value);
            }
            cropValues[4] = Convert.ToInt32(360);
            cropValues[5] = Convert.ToInt32(360);

            Session["cropButton"] = photoId;
            Session["cropValues"] = cropValues;
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string name = button.ID.Split('{', '}')[1];
            int num = int.Parse(button.ID.Split('{', '}')[2]); // photo ID

            Domain.ShoppingbasketItem found = null;
            foreach (Domain.ShoppingbasketItem item in shoppingCart)
            {
                if (item.PhotoID == num && item.Filter == filters[num])
                {
                    found = item;
                    break;
                }
            }
            if (found != null)
            {
                found.Quantity++;
            }
            else
            {
                PurchaseController purchaseController = new PurchaseController();
                int product = ProductTypes.getInt(products[num].ToString());
                int price = purchaseController.getPrice(product, num);
                ShoppingbasketItem newItem = new ShoppingbasketItem(num, name, filters[num], products[num], price);
                shoppingCart.Add(newItem);
            }

            if (Session["cropValues"] != null && Session["cropButton"] != null)
            {
                string photoid = (string)Session["cropButton"];
                cropValues = (int[])Session["cropValues"];

                foreach (Domain.ShoppingbasketItem item in shoppingCart)
                {
                    if (item.PhotoID.ToString() == photoid)
                    {
                        item.setCropValues("&" + cropValues[0].ToString() + " " + cropValues[1].ToString() + " " + cropValues[2].ToString() + " " + cropValues[3].ToString() + " " + cropValues[4].ToString() + " " + cropValues[5].ToString());
                        break;
                    }
                }
                Session["shoppingCart"] = shoppingCart;
            }
        }

        void DDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(ddl.ID);

            int num = int.Parse(match.Groups["Numeric"].Value);
            // set EType on ID
            products[num] = ProductTypes.getPType(ddl.SelectedValue);
        }

        void FilterChange(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(button.ID);

            string name = match.Groups["Alpha"].Value;
            int num = int.Parse(match.Groups["Numeric"].Value);

            // set name on ID
            filters[num] = FilterTypes.GetFType(name);
            // change color
            ColorChange(num);
        }
        void ColorChange(int num)
        {
            foreach (Domain.Photo photo in photos)
            {
                if (photo.ID == num)
                {
                    switch (filters[num])
                    {
                        case FilterTypes.FTypes.COLOR:
                            ConvertColor(photo);
                            break;
                        case FilterTypes.FTypes.BLACKWHITE:
                            ConvertBlackWhite(photo);
                            break;
                        case FilterTypes.FTypes.SEPIA:
                            ConvertSepia(photo);
                            break;
                        default:
                            break;
                    }

                    break;
                }
            }
        }

        private void ConvertColor(Domain.Photo photo)
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

        private void ConvertSepia(Domain.Photo photo)
        {
            this._current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Image.ToString()));
            Bitmap temp = (Bitmap)this._current;
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
            this._current = (Bitmap)bmap.Clone();
            this._current.Save(Server.MapPath("Sepia" + photo.ID + ".png"));

            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
                        {
                            currentImage.ImageUrl = "Sepia" + photo.ID + ".png";
                            break;
                        }
                    }
                }
            }
        }

        private void ConvertBlackWhite(Domain.Photo photo)
        {
            this._current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Image.ToString()));
            Bitmap temp = (Bitmap)this._current;
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
            this._current = (Bitmap)bmap.Clone();
            Random rnd = new Random();
            int a = rnd.Next();
            this._current.Save(Server.MapPath("BlackWhite" + photo.ID + ".png"));

            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
                        {
                            currentImage.ImageUrl = "BlackWhite" + photo.ID + ".png";
                            break;
                        }
                    }
                }
            }
        }
    }
}