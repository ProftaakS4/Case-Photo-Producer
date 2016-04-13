using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;
using System.Drawing;

namespace PhotoshopWebsite
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        // create instance of the photoController for future database connections through busisness layer
        PhotoController photoController = new PhotoController();

        // create a list of all the current user photos
        List<Domain.Photo> photos;
        List<Domain.Photo> searchedPhotos;
        private Bitmap _current;
        private int number;
        //private List<Product> testproducts;
        public Dictionary<Domain.Photo, int> shoppingCart
        {
            get
            {
                if (!(Session["shoppingCart"] is Dictionary<Domain.Photo, int>))
                {
                    Session["shoppingCart"] = new Dictionary<Domain.Photo, int>();
                }

                return (Dictionary<Domain.Photo, int>)Session["shoppingCart"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["searchedPhotos"] is List<Domain.Photo>)
            {
                searchedPhotos =  Session["searchedPhotos"] as List<Domain.Photo>;
            }
            // cast the session into the current user
            User currenUser = (User)Session["UserData"];

            // get the userid of the current user
            string userID = Convert.ToString(currenUser.ID);

            // get all the photoID's of the current user
            List<string> photoIDS = photoController.getUserPhotoIDs(userID);

            // get all the photos of the current user and add them to a list
            List<Domain.Photo> photos = new List<Domain.Photo>();
            if(photoIDS != null)
            {
                foreach (string s in photoIDS)
                {
                    photos.Add(photoController.getPhoto(s));
                }
            }
            else
            {
                Response.Write("No Photo's of user");
            }

            // store all the photos in the session
            Session["PhotosList"] = photos;

            // check if a search has taken place
            if(searchedPhotos != null && searchedPhotos.Count > 0)
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
            btnAddToCart.ID = x.ID.ToString();
            btnAddToCart.CssClass = "btn btn-default";
            btnAddToCart.Click += btnAddToCart_Click;
            btnAddToCart.Height = 30;
            btnAddToCart.Text = "Order";

            Button btnSepia = new Button();
            btnSepia.ID = "btnSepia" + x.ID;
            btnSepia.CssClass = "btn btn-default";
            btnSepia.Click += btnSepia_Click;
            btnSepia.Height = 30;
            btnSepia.Text = "Sepia";

            Button btnBlackWhite = new Button();
            btnBlackWhite.ID = "btnBlackWhite" + x.ID;
            btnBlackWhite.CssClass = "btn btn-default";
            btnBlackWhite.Click += btnBlackWhite_Click;
            btnBlackWhite.Height = 30;
            btnBlackWhite.Text = "Black & White";

            Button btnColor = new Button();
            btnColor.ID = "btnColor" + x.ID;
            btnColor.CssClass = "btn btn-default";
            btnColor.Click += btnColor_Click;
            btnColor.Height = 30;
            btnColor.Text = "Color";

            System.Web.UI.WebControls.Image imgProduct = new System.Web.UI.WebControls.Image();
            imgProduct.ID = "image" + x.ID.ToString();
            imgProduct.AlternateText = "No Image found, please contact administrator";
            imgProduct.ImageUrl = x.Path;
            imgProduct.Height = 200;
            imgProduct.Width = 330;

            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");
            //adding other div elements containing discriptions
            string div;
            if (number > 4)
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
            firstControl.Controls.Add(btnAddToCart);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnColor);
            pnlProduct.Controls.Add(firstControl);
            lastControl.InnerHtml = "</div> </div>  </div>";
            pnlProduct.Controls.Add(lastControl);
        }

        void btnColor_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                List<Domain.Photo> userPhotos = (List<Domain.Photo>)Session["PhotosList"];
                foreach (Domain.Photo photo in userPhotos)
                {
                    if ("btnColor" + photo.ID.ToString() == button.ID)
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
                                        currentImage.ImageUrl = photo.Path;
                                    }
                                }
                            }
                        }
                        break;
                    }                   
                }
            }
        }

        void btnAddToCart_Click(object sender, EventArgs e)
        {
            Button x = sender as Button;
            string id = x.ID;
            List<Domain.Photo> userPhotos = (List<Domain.Photo>)Session["PhotosList"];
            foreach (Domain.Photo photo in userPhotos)
            {
                if (photo.ID.ToString() == id)
                {
                    if (shoppingCart.Count == 0)
                    {
                        shoppingCart.Add(photo, 1);
                    }
                    else
                    {
                        if (shoppingCart.ContainsKey(photo))
                        {
                            shoppingCart[photo]++;
                        }
                        else
                        {
                            shoppingCart.Add(photo, 1);
                        }
                    }
                }
            }
            HttpContext.Current.Session["shoppingCart"] = shoppingCart;
            Response.Redirect(Request.RawUrl);
        }


        void btnSepia_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                List<Domain.Photo> userPhotos = (List<Domain.Photo>)Session["PhotosList"];
                foreach (Domain.Photo photo in userPhotos)
                {
                    if ("btnSepia" + photo.ID.ToString() == button.ID)
                    {
                        convertSepia(photo);
                        break;
                    }
                }
            }
        }


        void btnBlackWhite_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                List<Domain.Photo> userPhotos = (List<Domain.Photo>)Session["PhotosList"];
                foreach (Domain.Photo photo in userPhotos)
                {
                    if ("btnBlackWhite" + photo.ID.ToString() == button.ID)
                    {
                        convertBlackWhite(photo);
                        break;
                    }
                }
            }
        }

        private void convertSepia(Domain.Photo photo)
        {
            _current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Path.ToString()));
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
            _current.Save(Server.MapPath("../Images/Sepia" + photo.ID +".png"));

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
                        }
                    }
                }
            }
            //returnimage = (System.Drawing.Image)btm;
            //return returnimage;
        }

        private void convertBlackWhite(Domain.Photo photo)
        {
            _current = (Bitmap)Bitmap.FromFile(Server.MapPath(photo.Path.ToString()));
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
            _current.Save(Server.MapPath("../Images/BlackWhite"+ photo.ID + ".png"));

            foreach (HtmlGenericControl control in pnlProduct.Controls)
            {
                foreach (Control item in control.Controls)
                {
                    if (item is System.Web.UI.WebControls.Image)
                    {
                        System.Web.UI.WebControls.Image currentImage = item as System.Web.UI.WebControls.Image;
                        if (currentImage.ID.ToString() == "image" + photo.ID.ToString())
        {
                            currentImage.ImageUrl = "../Images/BlackWhite"+ photo.ID + ".png";
                        }
                    }
                }
            }
        }
    }
}




