using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Controller
{
    public class Product
    {
        public int ID { get; set; }
        public String Type { get; set; }
        public String Material { get; set; }
        public String Description { get; set; }
        public String  Image { get; set; }
        public int Stock { get; set; }

        public Product(int ID, String Type, String Material, String Description, String Image, int Stock)
        {
            this.ID = ID;
            this.Type = Type;
            this.Material = Material;
            this.Description = Description;
            this.Image = Image;
            this.Stock = Stock;
        }
    }
}