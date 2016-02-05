using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
         ___   
     ___/14 \___
 ___/ 8 \___/19 \___
/ 2 \___/13 \___/24 \
\___/ 7 \___/18 \___/
/ 1 \___/12 \___/23 \
\___/ 6 \___/17 \___/
/ 0 \___/11 \___/22 \
\___/ 5 \___/16 \___/
    \___/10 \___/
        \___/
*/

public class RunicBoard {

    Dictionary<uint, Rune> _runesInHand;
    Dictionary<uint, Rune> _runesOnBoard;

    public Dictionary<uint, Rune> RunesInHand
    {
        get
        {
            return _runesInHand;
        }
    }

    public Dictionary<uint, Rune> RunesOnBoard
    {
        get
        {
            return _runesOnBoard;
        }
    }

    /// <summary>
    /// Display in console runes in Hand 
    /// </summary>
    public void LogHand()
    {
        Logger.Debug("*** RUNES IN HAND ***");
        foreach (KeyValuePair<uint, Rune> kvp in _runesInHand)
        {
            Logger.Debug("Element " + kvp.Value.Element._name + ", Position Hand " + kvp.Value.PositionInHand + ", Position Board " + kvp.Value.PositionOnBoard);
        }
    }

    /// <summary>
    /// Display in console runes on board
    /// </summary>
    public void LogRunesOnBoard()
    {
        Logger.Debug("*** RUNES ON BOARD ***");
        foreach (KeyValuePair<uint, Rune> kvp in _runesOnBoard)
        {
            Logger.Debug("Element " + kvp.Value.Element._name + ", Position " + kvp.Key);
        }
    }

    public RunicBoard()
    {
        _runesInHand = new Dictionary<uint, Rune>();
        _runesOnBoard = new Dictionary<uint, Rune>();
    }

    public RunicBoard(Dictionary<uint, Rune> hand)
    {
        _runesInHand = hand;
        _runesOnBoard = new Dictionary<uint, Rune>();
    }

    public void Testing()
    {
        Logger.logLvl = Logger.Type.TRACE;
        //Testing !
        Rune r1 = new Rune(Element.GetElement(0), -1, 0);
        _runesInHand.Add(r1.PositionInHand, r1);
        Rune r2 = new Rune(Element.GetElement(1), -1, 1);
        _runesInHand.Add(r2.PositionInHand, r2);
        Rune r3 = new Rune(Element.GetElement(2), -1, 2);
        _runesInHand.Add(r3.PositionInHand, r3);
        Rune r4 = new Rune(Element.GetElement(2), -1, 3);
        _runesInHand.Add(r4.PositionInHand, r4);
        Rune r5 = new Rune(Element.GetElement(3), -1, 4);
        _runesInHand.Add(r5.PositionInHand, r5);
        Rune r6 = new Rune(Element.GetElement(3), -1, 5);
        _runesInHand.Add(r6.PositionInHand, r6);

        //LogHand();
        
        //PlaceRuneOnBoard(0, 12);
        //PlaceRuneOnBoard(1, 13);
        //PlaceRuneOnBoard(2, 14);
        //PlaceRuneOnBoard(3, 6);
        //PlaceRuneOnBoard(4, 5);

        //RemoveRuneFromBoard(4);

        //RemoveAllRunes();

        //ChangeRunePosition(4, 15);

        //List<uint> explored = new List<uint>();
        //Debug.Log("Connected to center ? " + IsConnectedToCenter(0, ref explored, ref runesOnBoard));

        //ChangeRunePosition(4, 5);

        LogHand();
        LogRunesOnBoard();

        //Logger.Debug("Can launch spell ? " + CanLaunchSpell());
    }

    /// <summary>
    /// Verify if you can place a rune at the position, and place it if it is OK.
    /// </summary>
    /// <param name="index">The index in runesInHand list</param>
    /// <param name="position">The position where the rune will be placed</param>
    /// <returns>Where the rune was placed on the board</returns>
    public int PlaceRuneOnBoard(uint index, uint position)
    {
        Rune rune;
        if(_runesInHand.TryGetValue(index, out rune))
        {
            // If no runes are on the board, places the rune in the center
            if (_runesOnBoard.Count == 0)
            {
                _runesOnBoard.Add(12, rune);
                rune.PositionOnBoard = 12;
                _runesInHand.Remove(index);
                return 12;
            }

            // The player cannot place a rune if a rune is already in place at the position
            if (_runesOnBoard.ContainsKey(position))
            {
                return -1;
            }

            // If runes are placed around this position, place the rune
            List<uint> neighbours = GetAdjacentPositions(position);
            for(int i = 0; i < neighbours.Count; i++)
            {
                if (_runesOnBoard.ContainsKey(neighbours[i]))
                {
                    _runesOnBoard.Add(position, rune);
                    rune.PositionOnBoard = (int)position;
                    _runesInHand.Remove(index);
                    return (int)position;
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Remove a rune from the board at this position, and put in back in the hand.
    /// </summary>
    /// <param name="position">The position where the rune is expected to be</param>
    /// <returns>If the rune was succesfully removed</returns>
    public bool RemoveRuneFromBoard(uint position)
    {
        Rune rune;
        bool runeFound = _runesOnBoard.TryGetValue(position, out rune);
        if (runeFound)
        {
            _runesOnBoard.Remove(position);
            _runesInHand.Add(rune.PositionInHand, rune);
            rune.PositionOnBoard = -1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Change a rune's position if it doesn't break the chain to the center.
    /// </summary>
    /// <param name="actualPosition">The actual position if the rune to move</param>
    /// <param name="newPosition">Where to place the rune</param>
    /// <returns>If the rune was moved succefully</returns>
    public bool ChangeRunePosition(uint actualPosition, uint newPosition)
    {
        Rune runeToMove;
        if (_runesOnBoard.TryGetValue(actualPosition, out runeToMove))
        {
            if (!_runesOnBoard.ContainsKey(newPosition))
            {
                // Copy of the runes on board without the rune we want to move
                Dictionary<uint, Rune> tempRunesOnBoard = new Dictionary<uint, Rune>(_runesOnBoard);
                tempRunesOnBoard.Remove(actualPosition);
                tempRunesOnBoard.Add(newPosition, runeToMove);
                //List<uint> explored = new List<uint>();

                if (EverythingIsConnecterToCenter(ref tempRunesOnBoard))
                {
                    _runesOnBoard.Add(newPosition, runeToMove);
                    _runesOnBoard.Remove(actualPosition);
                    runeToMove.PositionOnBoard = (int)newPosition;
                    Logger.Debug("Rune moved from " + actualPosition + " to " + newPosition);
                    return true;
                }
                else
                {
                    Logger.Debug("Could not move rune from " + actualPosition + " to " + newPosition);
                }
            }
            else
            {
                Logger.Debug("A rune is already on " + newPosition);
            }
        }
        else
        {
            Logger.Error("ChangeRunePosition : no rune detected at " + actualPosition);
            return false;
        }

        return false;
    }

    /// <summary>
    /// Remove all runes from the runic board and put them back in the hand.
    /// </summary>
    public void RemoveAllRunes()
    {
        foreach(KeyValuePair<uint, Rune> kvp in _runesOnBoard)
        {
            Rune rune = kvp.Value;
            rune.PositionOnBoard = -1;
            _runesInHand.Add(rune.PositionInHand, rune);
        }
        _runesOnBoard.Clear();
    }

    /// <summary>
    /// Check if the position exists on the board.
    /// </summary>
    /// <param name="p">The position</param>
    /// <returns></returns>
    private bool PositionExists(uint p)
    {
        if (p != 3 && p != 4 && p != 9 && p != 15 && p != 20 && p != 21 && p >= 0 && p <= 24)
            return true;
        return false;
    }

    /// <summary>
    /// Get adjacent positions to the one in parameter. 
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private List<uint> GetAdjacentPositions(uint position)
    {
        List<uint> neighbours = new List<uint>();

        uint n1 = position - 6;
        if (PositionExists(n1))
            neighbours.Add(n1);

        uint n2 = position - 5;
        if (PositionExists(n2))
            neighbours.Add(n2);

        uint n3 = position - 1;
        if (PositionExists(n3))
            neighbours.Add(n3);

        uint n4 = position + 1;
        if (PositionExists(n4))
            neighbours.Add(n4);

        uint n5 = position + 5;
        if (PositionExists(n5))
            neighbours.Add(n5);

        uint n6 = position + 6;
        if (PositionExists(n6))
            neighbours.Add(n6);

        return neighbours;
    }

    /// <summary>
    /// Get positions of runes adjacent to the position in parameter using runesOnBoard
    /// </summary>
    /// <param name="position">The position to test</param>
    /// <returns></returns>
    private List<uint> GetNeighboursPosition(uint position)
    {
        return GetNeighboursPosition(position, _runesOnBoard);
    }

    /// <summary>
    /// get positions of runes adjacent to the position in paramater, using the board in paramater
    /// </summary>
    /// <param name="position">The position to test</param>
    /// <param name="board">The board to test</param>
    /// <returns></returns>
    private List<uint> GetNeighboursPosition(uint position, Dictionary<uint, Rune> board)
    {
        List<uint> positions = GetAdjacentPositions(position);
        List<uint> neighboursPosition = new List<uint>();

        for (int i = 0; i < positions.Count; i++)
        {
            Rune rune;
            if (board.TryGetValue(positions[i], out rune))
            {
                neighboursPosition.Add(positions[i]);
            }
        }
        return neighboursPosition;
    }

    /// <summary>
    /// Check if every runes placed on the board are correctly connected to the center
    /// </summary>
    /// <param name="board">The board to test</param>
    /// <returns>true if everything is connected, false otherwise</returns>
    public bool EverythingIsConnecterToCenter(ref Dictionary<uint, Rune> board)
    {
        bool isConnected = true;
        foreach(KeyValuePair<uint, Rune> kvp in board)
        {
            List<uint> explored = new List<uint>();
            isConnected = IsConnectedToCenter(kvp.Key, ref explored, ref board);
            Logger.Debug(kvp.Key + " connected ? " + isConnected);
            if (!isConnected)
                return false;
        }
        return isConnected;
    }

    /// <summary>
    /// Return if the position is connected to the center
    /// </summary>
    /// <param name="position">The position to test</param>
    /// <param name="explored">List of explored positions</param>
    /// <param name="board">The board to test</param>
    /// <returns></returns>
    public bool IsConnectedToCenter(uint position, ref List<uint> explored, ref Dictionary<uint, Rune> board)
    {
        if (position == 12)
        {
            return true;
        }

        if (explored == null)
        {
            explored = new List<uint>();
        }

        explored.Add(position);
        List<uint> positions = GetNeighboursPosition(position, board);

        for (int i = 0; i < positions.Count; i++)
        {
            if (!explored.Contains(positions[i]))
            {
                bool connected = IsConnectedToCenter(positions[i], ref explored, ref board);
                if (connected)
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A Queue of Element from runes that are on the board.</returns>
    public Queue<Element> GetSortedElementQueue()
    {
        List<Element> elementsList = new List<Element>();
        foreach (KeyValuePair<uint, Rune> kvp in _runesOnBoard)
        {
            elementsList.Add(kvp.Value.Element);
        }
        elementsList.Sort();

        Queue<Element> elementsQueue = new Queue<Element>(elementsList);

        return elementsQueue;
    }

    /// <summary>
    /// Check if the combination of runes exists in the database
    /// </summary>
    /// <returns></returns>
    public bool CanLaunchSpell()
    {
        Queue<Element> elements = GetSortedElementQueue();
        Logger.Debug("*** Elements in queue ***");
        //foreach(Element e in elements)
        //{
        //    Logger.Debug("E : " + e._id + ", " + e._name);
        //}
        SpellManager sm = SpellManager.getInstance();
        if (sm.ElementNode.GetSelfSpell(elements) != null)
            return true;

        return false;
    }
}
