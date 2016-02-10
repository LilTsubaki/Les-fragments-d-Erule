using UnityEngine;
using System.Collections;

public class Rune
{
    private Element _element;
    private int _positionOnBoard;
    private int _positionInHand;

    public Element Element
    {
        get
        {
            return _element;
        }

        set
        {
            _element = value;
        }
    }

    public int PositionOnBoard
    {
        get
        {
            return _positionOnBoard;
        }

        set
        {
            _positionOnBoard = value;
        }
    }

    public int PositionInHand
    {
        get
        {
            return _positionInHand;
        }

        set
        {
            _positionInHand = value;
        }
    }

    public Rune(Element element, int positionOnBoard, int positionInHand)
    {
        Element = element;
        PositionOnBoard = positionOnBoard;
        PositionInHand = positionInHand;
    }

    public bool IsOnBoard()
    {
        return _positionOnBoard >= 0;
    }
}
