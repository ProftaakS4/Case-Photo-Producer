using PhotoshopWebsite.Controller;
using PhotoshopWebsite.Domain;
using PhotoshopWebsite.Enumeration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PhotoshopWebsite.Gui.Photographer
{
    [ExcludeFromCodeCoverage]
    public partial class Pictures : System.Web.UI.Page
    {
        // create instance of the photoController for future database connections through busisness layer
        PhotoController photoController = new PhotoController();

        // create a list of all the current user photos
        public List<Domain.Photo> photos;

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
            string userID = Convert.ToString(currenUser.ID);

            // get all the photoID's of the current user
            List<int> photoIDS = photoController.getPhotoGrapherPhotoIDs(userID);

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

            // fill the page with the users photos
            foreach (Domain.Photo photo in photos)
            {
                Fillpage(photo);
            }
        }


        private void Fillpage(Domain.Photo x)
        {
            //create buttons
            RadioButton btnSepia = new RadioButton();
            btnSepia.ID = "SEPIA" + x.ID;
            btnSepia.CheckedChanged += filterChange;
            btnSepia.AutoPostBack = true;
            btnSepia.GroupName = x.ID.ToString();
            btnSepia.Height = 30;
            btnSepia.Text = "Sepia ";

            RadioButton btnBlackWhite = new RadioButton();
            btnBlackWhite.ID = "BLACKWHITE" + x.ID;
            btnBlackWhite.CheckedChanged += filterChange;
            btnBlackWhite.AutoPostBack = true;
            btnBlackWhite.GroupName = x.ID.ToString();
            btnBlackWhite.Height = 30;
            btnBlackWhite.Text = "Black & White ";

            RadioButton btnColor = new RadioButton();
            btnColor.ID = "COLOR" + x.ID;
            btnColor.CheckedChanged += filterChange;
            btnColor.AutoPostBack = true;
            btnColor.GroupName = x.ID.ToString();
            btnColor.Height = 30;
            btnColor.Text = "Color ";

            Button btnDownload = new Button();
            btnDownload.ID = "download" + x.ID;
            btnDownload.Text = "Download";
            btnDownload.CssClass = "btn btn-default";
            btnDownload.Click += btnDownload_Click;


            if (!filters.ContainsKey(x.ID))
            {
                filters.Add(x.ID, FilterTypes.FTypes.COLOR);
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
            colorChange(x.ID);

            System.Web.UI.WebControls.Image imgProduct = new System.Web.UI.WebControls.Image();
            imgProduct.ID = "image" + x.ID.ToString();
            imgProduct.AlternateText = "No Image found, please the contact administrator";
            imgProduct.ImageUrl = x.Image;
            imgProduct.Height = 200;
            imgProduct.Width = 330;
            imgProduct.CssClass = "img-responsive img-thumbnail";

            HtmlGenericControl firstControl = new HtmlGenericControl("div");
            HtmlGenericControl secondControl = new HtmlGenericControl("div");
            HtmlGenericControl lastControl = new HtmlGenericControl("div");

            String div = "<div class='col-sm-4'>";
            //add buttons
            firstControl.InnerHtml = div + "<div class='thumbnail' style='max-width:330px max-height:150px;'><div class='caption'>";

            secondControl.InnerHtml = "<p>" + x.Description + "</p>";
            firstControl.Controls.Add(imgProduct);
            firstControl.Controls.Add(secondControl);
            firstControl.Controls.Add(btnColor);
            firstControl.Controls.Add(btnBlackWhite);
            firstControl.Controls.Add(btnSepia);
            firstControl.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp&nbsp"));
            firstControl.Controls.Add(btnDownload);
            firstControl.Controls.Add(new LiteralControl("<br />"));
            pnlProduct.Controls.Add(firstControl);

            lastControl.InnerHtml = "</div></div></div>";
            pnlProduct.Controls.Add(lastControl);
        }

        void btnDownload_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(button.ID);
            int num = Int32.Parse(match.Groups["Numeric"].Value);

            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + photoController.getPhoto(num).ID+ ".jpeg");
            Response.TransmitFile(Server.MapPath(photoController.getPhoto(num).Image));
            Response.End();
        }

        void filterChange(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            Regex regex = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            Match match = regex.Match(button.ID);

            string name = match.Groups["Alpha"].Value;
            int num = Int32.Parse(match.Groups["Numeric"].Value);

            //set name on ID
            filters[num] = FilterTypes.getFType(name);
            //change color
            colorChange(num);
        }
        void colorChange(int num)
        {
            foreach (Domain.Photo photo in photos)
            {
                if (photo.ID == num)
                {
                    switch (filters[num])
                    {
                        case FilterTypes.FTypes.COLOR:
                            convertColor(photo);
                            break;
                        case FilterTypes.FTypes.BLACKWHITE:
                            convertBlackWhite(photo);
                            break;
                        case FilterTypes.FTypes.SEPIA:
                            convertSepia(photo);
                            break;
                        default:
                            break;
                    }
                    break;
                }
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
            _current.Save(Server.MapPath("Sepia" + photo.ID + ".png"));

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
            _current.Save(Server.MapPath("BlackWhite" + photo.ID + ".png"));

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
