using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.DatabaseTier
{
    public class ProductTypes
    {
        public enum ETypes
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

        static public ETypes getEType(string eType)
        {
            switch (eType)
            {
                case "PHOTO1x2":
                    return ETypes.PHOTO1x2;
                case "PHOTO2x4":
                    return ETypes.PHOTO2x4;
                case "PHOTO5x8":
                    return ETypes.PHOTO5x8;
                case "MUISMAT":
                    return ETypes.MUISMAT;
                case "TASSEN":
                    return ETypes.TASSEN;
                case "TSHIRT":
                    return ETypes.TSHIRT;
                case "MOK":
                    return ETypes.MOK;
                case "CANVAS":
                    return ETypes.CANVAS;
                case "DIBOND":
                    return ETypes.DIBOND;
                    //default to photo1x2
                default:
                    return ETypes.PHOTO1x2;
            }
        }
    }
}