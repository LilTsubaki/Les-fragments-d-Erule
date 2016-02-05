using UnityEngine;
using System.Collections;

public class Rune
{
    private Element _element;
    private int _positionOnBoard;
    private uint _positionInHand;

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

    public uint PositionInHand
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

    public Rune(Element element, int positionOnBoard, uint positionInHand)
    {
        Element = element;
        PositionOnBoard = positionOnBoard;
        PositionInHand = positionInHand;
    }
}
