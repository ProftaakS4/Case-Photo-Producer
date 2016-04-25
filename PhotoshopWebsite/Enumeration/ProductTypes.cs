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
    }
}