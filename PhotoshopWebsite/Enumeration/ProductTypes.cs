using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Enumeration
{
    public class ProductTypes
    {
        public enum PTypes
        {
            PHOTO1x2,
            PHOTO2x4,
            PHOTO5x8,
            MUISMAT,
            TASSEN,
            TSHIRT,
            MOK,
            CANVAS,
            DIBOND
        };

        static public PTypes getPType(string eType)
        {
            switch (eType)
            {
                case "PHOTO1x2":
                    return PTypes.PHOTO1x2;
                case "PHOTO2x4":
                    return PTypes.PHOTO2x4;
                case "PHOTO5x8":
                    return PTypes.PHOTO5x8;
                case "MUISMAT":
                    return PTypes.MUISMAT;
                case "TASSEN":
                    return PTypes.TASSEN;
                case "TSHIRT":
                    return PTypes.TSHIRT;
                case "MOK":
                    return PTypes.MOK;
                case "CANVAS":
                    return PTypes.CANVAS;
                case "DIBOND":
                    return PTypes.DIBOND;
                //default to photo1x2
                default:
                    return PTypes.PHOTO1x2;
            }
        }

        static public int getInt(string eType)
        {
            switch (eType)
            {
                case "PHOTO1x2":
                    return 1;
                case "PHOTO2x4":
                    return 2;
                case "PHOTO5x8":
                    return 3;
                case "MUISMAT":
                    return 4;
                case "TASSEN":
                    return 5;
                case "TSHIRT":
                    return 6;
                case "MOK":
                    return 7;
                case "CANVAS":
                    return 8;
                case "DIBOND":
                    return 9;
                //default to 1
                default:
                    return 1;
            }
        }


    }
}