using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhotoshopWebsite.Controller;


namespace PhotoshopWebsite.Gui.Client
{
    public partial class GreyscaleConverter : System.Web.UI.Page
    {
        private Bitmap _current;
        protected void Page_Load(object sender, EventArgs e)
        {
            Product testproduct1 = new Product(1, "PHOTO1x2", "PAPIER", "Foto van formaat 1x2", "../Images/Shoppingcart.png", -1);
            Img.ImageUrl = testproduct1.Image;

            _current = (Bitmap)Bitmap.FromFile(Server.MapPath(Img.ImageUrl.ToString()));
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filePath = Server.MapPath("Images/" + FileUpload1.FileName);
                FileUpload1.SaveAs(filePath);
                Img.ImageUrl = "Images/" + FileUpload1.FileName;
                Session["filepath"] = "Images/" + FileUpload1.FileName;
            }
            else
            {
                Response.Write("Please Select An Image!");
            }
        }

        protected void btnGreyScale_Click(object sender, EventArgs e)
        {
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
            _current.Save(Server.MapPath("../Images/" + a + ".png"));
            Img.ImageUrl = "../Images/" + a + ".png";

        }

        protected void btnSepia_Click(object sender, EventArgs e)
        {
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
            Random rnd = new Random();
            int a = rnd.Next();
            _current.Save(Server.MapPath("../Images/" + a + ".png"));
            Img.ImageUrl = "../Images/" + a + ".png";

           
        }
    }
}
