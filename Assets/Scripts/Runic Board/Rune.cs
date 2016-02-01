using UnityEngine;
using System.Collections;

public class Rune {

    internal Element element;
    internal int positionOnBoard;

    public bool IsOnBoard()
    {
        return (positionOnBoard != -1);
    }
}
