using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Direction
{
    public enum EnumDirection
    {
        East = 0,
        DiagonalSouthEast = 1,
        SouthEast = 2,
        DiagonalSouth = 3,
        SouthWest = 4,
        DiagonalSouthWest = 5,
        West = 6,
        DiagonalNorthWest = 7,
        NorthWest = 8,
        DiagonalNorth = 9,
        NorthEast = 10,
        DiagonalNorthEast = 11
    }

    public static EnumDirection stringToDirection(String strDir)
    {
        EnumDirection orientation = new EnumDirection();
        switch (strDir)
        {
            case "east":
                orientation = EnumDirection.East;
                break;
            case "diagonalSouthEast":
                orientation = EnumDirection.DiagonalSouthEast;
                break;
            case "southEast":
                orientation = EnumDirection.SouthEast;
                break;
            case "diagonalSouth":
                orientation = EnumDirection.DiagonalSouth;
                break;
            case "southWest":
                orientation = EnumDirection.SouthWest;
                break;
            case "diagonalSouthWest":
                orientation = EnumDirection.DiagonalSouthWest;
                break;
            case "west":
                orientation = EnumDirection.West;
                break;
            case "diagonalNorthWest":
                orientation = EnumDirection.DiagonalNorthWest;
                break;
            case "northWest":
                orientation = EnumDirection.NorthWest;
                break;
            case "diagonalNorth":
                orientation = EnumDirection.DiagonalNorth;
                break;
            case "northEast":
                orientation = EnumDirection.NorthEast;
                break;
            case "diagonalNorthEast":
                orientation = EnumDirection.DiagonalNorthEast;
                break;
        }
        return orientation;
    }

}

