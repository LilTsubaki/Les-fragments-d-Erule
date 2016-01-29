using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Orientation
{ 
    public enum EnumOrientation
    {
        Line,
        Diagonal,
        Any
    };

    public static EnumOrientation stringToOrientation(String strOri)
    {
        EnumOrientation orientation = new EnumOrientation();
        switch (strOri)
        {
            case "line":
                orientation = EnumOrientation.Line;
                break;
            case "diagonal":
                orientation = EnumOrientation.Diagonal;
                break;
            case "any":
                orientation = EnumOrientation.Any;
                break;
        }
        return orientation;
    }

}
