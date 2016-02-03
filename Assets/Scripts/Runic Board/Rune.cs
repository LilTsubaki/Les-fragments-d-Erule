using UnityEngine;
using System.Collections;

public class Rune : MonoBehaviour
{
    internal Element _element;
    internal int _positionOnBoard;

    public Rune(Element element, int positionOnBoard)
    {
        _element = element;
        _positionOnBoard = positionOnBoard;
    }
}
