using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoshopWebsite.Enumeration;
using System.Data;

namespace PhotoshopWebsite.Domain
{
    /// <summary>
    /// Photo class is create to create images that can be ordered
    /// </summary>
    [Serializable]
    public class Photo
    {
        private DatabaseTier.QueryDatabase database = new DatabaseTier.QueryDatabase();

        public int ID { get; set; }
        public int PhotographerID { get; set; }
        public int MapID { get; set; }
        public string Image { get; set; }
        public string Resolution { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// create a user with all possible variables
        /// </summary>
        /// <param name="id">the id of the photo</param>
        /// <param name="photographerID">the id of the photographer to whom the photo belongs to</param>
        /// <param name="map_ID">the map id of the photo</param>
        /// <param name="image">the image path of the photo</param>
        /// <param name="resolution">the resolution of the photo</param>
        /// <param name="description">the description of the photo</param>
        public Photo(int id, int photographerID, int map_ID, string image, string resolution, string description)
        {
            this.ID = id;
            this.PhotographerID = photographerID;
            this.MapID = map_ID;
            this.Image = image;
            this.Resolution = resolution;
            this.Description = description;
        }

        /// <summary>
        /// returns the type of a photo searched for by ID
        /// </summary>
        /// <param name="photoID">the id of the photo</param>
        /// <returns></returns>
        public List<ProductTypes.PTypes> getTypes(int photoID)
        {
            List<ProductTypes.PTypes> types = new List<ProductTypes.PTypes>();

            Dictionary<string, string[]> parameters = new Dictionary<string, string[]>();
            parameters.Add("p_id", new string[] { "string", photoID.ToString() });
            DataTable dt = this.database.CallProcedure("getAccountIdperPhoto", parameters);
            if (dt.Rows.Count == 0)
            {
                return types;
            }
            parameters = new Dictionary<string, string[]>();
            parameters.Add("a_id", new string[] { "string", dt.Rows[0][0].ToString() });
            dt = this.database.CallProcedure("getProductTypesByAccountId", parameters);

            foreach (DataRow data in dt.Rows)
            {
                switch (data[0].ToString())
                {
                    case "1":
                        types.Add(ProductTypes.PTypes.PHOTO1x2);
                        break;
                    case "2":
                        types.Add(ProductTypes.PTypes.PHOTO2x4);
                        break;
                    case "3":
                        types.Add(ProductTypes.PTypes.PHOTO5x8);
                        break;
                    case "4":
                        types.Add(ProductTypes.PTypes.MUISMAT);
                        break;
                    case "5":
                        types.Add(ProductTypes.PTypes.TASSEN);
                        break;
                    case "6":
                        types.Add(ProductTypes.PTypes.TSHIRT);
                        break;
                    case "7":
                        types.Add(ProductTypes.PTypes.MOK);
                        break;
                    case "8":
                        types.Add(ProductTypes.PTypes.CANVAS);
                        break;
                    case "9":
                        types.Add(ProductTypes.PTypes.DIBOND);
                        break;
                }
            }
            return types;
        }
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }
            // If parameter cannot be cast to Point return false.
            Photo p = obj as Photo;
            if ((object)p == null)
            {
                return false;
            }
            // Return true if the fields match:
            return this.ID == p.ID;
        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}