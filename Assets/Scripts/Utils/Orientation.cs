using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Define the "orientation" for a spell
/// if enumOrientation is set to "Line" you only can target in line
/// if enumOrientation is set to "Diagonal" you only can target in diagonal
/// if enumOrientation is set to "Any" you can target anywhere
/// </summary>
class Orientation
{ 
    public enum EnumOrientation
    {
        Line,
        Diagonal,
        Any
    };

    /// <summary>
    /// return an enumOrientation from a string
    /// </summary>
    /// <param name="strOri"></param>
    /// <returns></returns>
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
