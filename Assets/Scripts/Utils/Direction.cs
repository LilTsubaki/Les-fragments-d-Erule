using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Define the a direction for a spell pattern
/// </summary>
public class Direction
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
        DiagonalNorthEast = 11,
        Default = 12
    }

    /// <summary>
    /// return an EnumDirection from a string
    /// </summary>
    /// <param name="strDir"></param>
    /// <returns></returns>
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
            case "default":
                orientation = EnumDirection.Default;
                break;
        }
        return orientation;
    }

    public static List<EnumDirection> GetLineEnum()
    {
        List<EnumDirection> directions = new List<EnumDirection>();
        directions.Add(EnumDirection.East);
        directions.Add(EnumDirection.SouthEast);
        directions.Add(EnumDirection.SouthWest);
        directions.Add(EnumDirection.West);
        directions.Add(EnumDirection.NorthWest);
        directions.Add(EnumDirection.NorthEast);
        return directions;
    }

    public static List<EnumDirection> GetDiagonalEnum()
    {
        List<EnumDirection> directions = new List<EnumDirection>();
        directions.Add(EnumDirection.DiagonalNorth);
        directions.Add(EnumDirection.DiagonalNorthEast);
        directions.Add(EnumDirection.DiagonalNorthWest);
        directions.Add(EnumDirection.DiagonalSouth);
        directions.Add(EnumDirection.DiagonalSouthEast);
        directions.Add(EnumDirection.DiagonalSouthWest);
        return directions;
    }

    public static List<EnumDirection> GetAnyEnum()
    {
        List<EnumDirection> directions = new List<EnumDirection>();
        directions.Add(EnumDirection.East);
        directions.Add(EnumDirection.SouthEast);
        directions.Add(EnumDirection.SouthWest);
        directions.Add(EnumDirection.West);
        directions.Add(EnumDirection.NorthWest);
        directions.Add(EnumDirection.NorthEast);
        directions.Add(EnumDirection.DiagonalNorth);
        directions.Add(EnumDirection.DiagonalNorthEast);
        directions.Add(EnumDirection.DiagonalNorthWest);
        directions.Add(EnumDirection.DiagonalSouth);
        directions.Add(EnumDirection.DiagonalSouthEast);
        directions.Add(EnumDirection.DiagonalSouthWest);
        return directions;
    }

    public static int GetDiff(EnumDirection source, EnumDirection dest)
    {
        //Logger.Error(((dest - source) + 12) % 12);
        return ((dest - source)+12)%12;
    }

    public static EnumDirection GetDirection(Hexagon source, Hexagon destination)
    {
        Logger.Debug("source : " + source._posX + " | " + source._posY);
        Logger.Debug("destination : " + destination._posX + " | " + destination._posY);
        //TOFINISH
        if (source._posY == destination._posY)
        {
            if (destination._posX > source._posX)
                return EnumDirection.East;
            if (destination._posX < source._posX)
                return EnumDirection.West;
        }
        if (destination._posX-source._posX == destination._posY-source._posY)
        {
            if (destination._posX > source._posX)
                return EnumDirection.NorthEast;
            if (destination._posX < source._posX)
                return EnumDirection.SouthWest;
        }
        if(source._posX == destination._posX)
        {
            if (destination._posY > source._posY)
                return EnumDirection.NorthWest;
            if (destination._posY < source._posY)
                return EnumDirection.SouthEast;
        }

        //TO TEST
        if (destination._posX < source._posX && Math.Abs(destination._posY) == Math.Abs(destination._posX))
            return EnumDirection.DiagonalNorthWest;

        if (destination._posX > source._posX && Math.Abs(destination._posY) == Math.Abs(destination._posX))
            return EnumDirection.DiagonalSouthEast;

        if(destination._posX < source._posX && destination._posY < source._posY)
        {
            if (destination._posY == destination._posX / 2)
                return EnumDirection.SouthWest;
            if (destination._posX == destination._posY / 2)
                return EnumDirection.DiagonalSouth;
        }

        if (destination._posX > source._posX && destination._posY > source._posY)
        {
            if (destination._posY == destination._posX / 2)
                return EnumDirection.NorthEast;
            if (destination._posX == destination._posY / 2)
                return EnumDirection.DiagonalNorth;
        }

        return EnumDirection.Default;
    }

    public static EnumDirection Rotate(EnumDirection source, int rotateValue)
    {
        //Logger.Error("source : " + source.ToString() + " Dest : " + ((EnumDirection)(((int)source + rotateValue) % 12)).ToString());
        return (EnumDirection)(((int)source + rotateValue) % 12);
    }
}

