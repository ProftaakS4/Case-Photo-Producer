using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoshopWebsite.Enumeration
{
    public class FilterTypes
    {
        public enum FTypes
        {
            COLOR,
            BLACKWHITE,
            SEPIA
        };

        static public FTypes getFType(string eType)
        {
            switch (eType)
            {
                case "COLOR":
                    return FTypes.COLOR;
                case "BLACKWHITE":
                    return FTypes.BLACKWHITE;
                case "SEPIA":
                    return FTypes.SEPIA;
                    //default to color
                default:
                    return FTypes.COLOR;
            }
        }
    }
}