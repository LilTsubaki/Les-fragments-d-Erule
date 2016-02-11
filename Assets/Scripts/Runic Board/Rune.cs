using UnityEngine;
using System.Collections;

public class Rune
{
    private Element _element;
    private int _positionOnBoard;
    private int _positionInHand;

    // The turn (given by TurnManager._turnNumber) when the rune was last placed.
    private int _turnUsed;

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

    public int TurnUsed
    {
        get
        {
            return _turnUsed;
        }

        set
        {
            _turnUsed = value;
        }
    }

    public Rune(Element element, int positionOnBoard, int positionInHand)
    {
        Element = element;
        PositionOnBoard = positionOnBoard;
        PositionInHand = positionInHand;
    }

    public Rune(Element element, int positionOnBoard)
    {
        Element = element;
        PositionOnBoard = positionOnBoard;
        PositionInHand = -1;
    }

    public bool IsOnBoard()
    {
        return _positionOnBoard >= 0;
    }
}
