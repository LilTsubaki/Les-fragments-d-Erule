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

    Dictionary<int, Rune> _runesInHand;
    Dictionary<int, Rune> _runesOnBoard;

    int _secondPlaced;

    private int _idPerfection = 22;
    private int _idSublimation = 14;
    private int _idStability = 0;

    public Dictionary<int, Rune> RunesInHand
    {
        get
        {
            return _runesInHand;
        }
    }

    public Dictionary<int, Rune> RunesOnBoard
    {
        get
        {
            return _runesOnBoard;
        }

        set
        {
            _runesOnBoard = value;
        }
    }

    public int SecondPlaced
    {
        get
        {
            return _secondPlaced;
        }

        set
        {
            _secondPlaced = value;
        }
    }

    /// <summary>
    /// Display in console runes in Hand 
    /// </summary>
    public void LogHand()
    {
        Logger.Debug("*** RUNES IN HAND ***");
        foreach (KeyValuePair<int, Rune> kvp in _runesInHand)
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
        foreach (KeyValuePair<int, Rune> kvp in _runesOnBoard)
        {
            Logger.Debug("Element " + kvp.Value.Element._name + ", Position " + kvp.Key);
        }
    }

    public RunicBoard()
    {
        _runesInHand = new Dictionary<int, Rune>();
        _runesOnBoard = new Dictionary<int, Rune>();
    }

    public RunicBoard(Dictionary<int, Rune> hand)
    {
        _runesInHand = hand;
        _runesOnBoard = new Dictionary<int, Rune>();
    }

    public void Testing()
    {
        //Logger.logLvl = Logger.Type.TRACE;
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

        //List<int> explored = new List<int>();
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
    public int PlaceRuneOnBoard(int index, int position)
    {
        if (ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints > 0 && ClientManager.GetInstance()._client.IsMyTurn && !ClientManager.GetInstance()._client.LockedMode)
        {
            Rune rune;
            if(_runesInHand.TryGetValue(index, out rune))
            {
                // If no runes are on the board, places the rune in the center
                if (_runesOnBoard.Count == 0)
                {
                    rune.TurnUsed = ClientManager.GetInstance()._client._turnNumber;
                    _runesOnBoard.Add(12, rune);
                    rune.PositionOnBoard = 12;
                    _runesInHand.Remove(index);
                    ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints--;
                    return 12;
                }

                // The player cannot place a rune if a rune is already in place at the position
                if (_runesOnBoard.ContainsKey(position))
                {
                    return -1;
                }

                // If runes are placed around this position, place the rune
                List<int> neighbours = GetAdjacentPositions(position);
                for(int i = 0; i < neighbours.Count; i++)
                {
                    if (_runesOnBoard.ContainsKey(neighbours[i]))
                    {
                        rune.TurnUsed = ClientManager.GetInstance()._client._turnNumber;
                        _runesOnBoard.Add(position, rune);
                        rune.PositionOnBoard = position;
                        _runesInHand.Remove(index);
                        ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints--;
                        if (_runesOnBoard.Count == 2) // Keep in mind what was the second rune placed for perfection pole
                            SecondPlaced = position;
                        return position;
                    }
                }
            }
        }
        else
        {
            if (ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints > 0)
                Logger.Error("Not Your turn");
            else
                if(!ClientManager.GetInstance()._client.LockedMode)
                    Logger.Error("Unlock Runic board !!");
                else
                    Logger.Error("Not enough action points");
        }
        return -1;
    }

    /// <summary>
    /// Remove a rune from the board at this position, and put in back in the hand.
    /// </summary>
    /// <param name="position">The position where the rune is expected to be</param>
    /// <returns>If the rune was succesfully removed</returns>
    public bool RemoveRuneFromBoard(int position, bool sendRemove = true)
    {
        if (!ClientManager.GetInstance()._client.IsMyTurn)
            return false;
        Rune rune;
        bool runeFound = _runesOnBoard.TryGetValue(position, out rune);
        if (runeFound)
        {
            if (rune.TurnUsed == ClientManager.GetInstance()._client._turnNumber)
            {
                Dictionary<int, Rune> tempRunesOnBoard = new Dictionary<int, Rune>(_runesOnBoard);
                tempRunesOnBoard.Remove(position);
                if (EverythingIsConnectedToCenter(ref tempRunesOnBoard))
                {
                    if (_runesOnBoard.Count == 2)
                        SecondPlaced = -1;
                    _runesOnBoard.Remove(position);
                    _runesInHand.Add(rune.PositionInHand, rune);
                    rune.PositionOnBoard = -1;
                    ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints++;
                    if(sendRemove)
                        ClientManager.GetInstance()._client.SendRemoveRune();
                    return true;
                }
                else
                {
                    Logger.Debug("Could not remove rune from " + position + " : not every runes are connected");
                }
            }
            else
            {
                Logger.Debug("Could not remove rune from " + position + " : the rune was not placed on this turn");
            }
        }
        return false;
    }

    /// <summary>
    /// Change a rune's position if it doesn't break the chain to the center.
    /// </summary>
    /// <param name="actualPosition">The actual position if the rune to move</param>
    /// <param name="newPosition">Where to place the rune</param>
    /// <returns>If the rune was moved succefully</returns>
    public bool ChangeRunePosition(int actualPosition, int newPosition)
    {
        Rune runeToMove;
        if (_runesOnBoard.TryGetValue(actualPosition, out runeToMove) && ClientManager.GetInstance()._client.IsMyTurn)
        {
            if (!_runesOnBoard.ContainsKey(newPosition))
            {
                // Copy of the runes on board without the rune we want to move
                Dictionary<int, Rune> tempRunesOnBoard = new Dictionary<int, Rune>(_runesOnBoard);
                tempRunesOnBoard.Remove(actualPosition);
                tempRunesOnBoard.Add(newPosition, runeToMove);
                //List<int> explored = new List<int>();

                if (EverythingIsConnectedToCenter(ref tempRunesOnBoard))
                {
                    _runesOnBoard.Add(newPosition, runeToMove);
                    _runesOnBoard.Remove(actualPosition);
                    runeToMove.PositionOnBoard = newPosition;
                    Logger.Debug("Rune moved from " + actualPosition + " to " + newPosition);
                    if (RunesOnBoard.Count == 2)
                        SecondPlaced = newPosition;
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
        foreach(KeyValuePair<int, Rune> kvp in _runesOnBoard)
        {
            Rune rune = kvp.Value;
            rune.PositionOnBoard = -1;
            _runesInHand.Add(rune.PositionInHand, rune);
        }

        //ClientManager.GetInstance()._client.CurrentCharacter.CurrentActionPoints += _runesOnBoard.Count;

        _runesOnBoard.Clear();

        SecondPlaced = -1;
    }

    /// <summary>
    /// Remove all runes that were placed on the current turn.
    /// </summary>
    public void RemoveAllRunesFromThisTurn()
    {
        List<int> markedToDelete = new List<int>();
        foreach (KeyValuePair<int, Rune> kvp in _runesOnBoard)
        {
            if (kvp.Value.TurnUsed == ClientManager.GetInstance()._client._turnNumber)
                markedToDelete.Add(kvp.Key);
        }

        for (int i = 0; i < markedToDelete.Count; i++)
        {
            _runesOnBoard.Remove(markedToDelete[i]);
        }

        if(_runesOnBoard.Count < 2)
        {
            SecondPlaced = -1;
        }
    }


    public void RemoveAllRunesExceptHistory(bool ignoreSecond)
    {
        List<int> ids = new List<int>();
        foreach (KeyValuePair<int, Rune> kvp in _runesOnBoard)
        {
            Rune rune = kvp.Value;
            if (rune.PositionOnBoard != 12 && (rune.PositionOnBoard != SecondPlaced || ignoreSecond))
            {
                ids.Add(rune.PositionOnBoard);
                rune.PositionOnBoard = -1;
                _runesInHand.Add(rune.PositionInHand, rune);
            }
            else
            {
                rune.TurnUsed = -1;
            }
        }

        for(int i = 0; i < ids.Count; ++i)
        {
            _runesOnBoard.Remove(ids[i]);
        }
    }

    /// <summary>
    /// Check if the position exists on the board.
    /// </summary>
    /// <param name="p">The position</param>
    /// <returns></returns>
    private bool PositionExists(int p)
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
    private List<int> GetAdjacentPositions(int position)
    {
        List<int> neighbours = new List<int>();

        int n1 = position - 6;
        if (PositionExists(n1))
            neighbours.Add(n1);

        int n2 = position - 5;
        if (PositionExists(n2))
            neighbours.Add(n2);

        int n3 = position - 1;
        if (PositionExists(n3))
            neighbours.Add(n3);

        int n4 = position + 1;
        if (PositionExists(n4))
            neighbours.Add(n4);

        int n5 = position + 5;
        if (PositionExists(n5))
            neighbours.Add(n5);

        int n6 = position + 6;
        if (PositionExists(n6))
            neighbours.Add(n6);

        return neighbours;
    }

    /// <summary>
    /// Get positions of runes adjacent to the position in parameter using runesOnBoard
    /// </summary>
    /// <param name="position">The position to test</param>
    /// <returns></returns>
    private List<int> GetNeighboursPosition(int position)
    {
        return GetNeighboursPosition(position, _runesOnBoard);
    }

    /// <summary>
    /// get positions of runes adjacent to the position in paramater, using the board in paramater
    /// </summary>
    /// <param name="position">The position to test</param>
    /// <param name="board">The board to test</param>
    /// <returns></returns>
    private List<int> GetNeighboursPosition(int position, Dictionary<int, Rune> board)
    {
        List<int> positions = GetAdjacentPositions(position);
        List<int> neighboursPosition = new List<int>();

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
    public bool EverythingIsConnectedToCenter(ref Dictionary<int, Rune> board)
    {
        bool isConnected = true;
        foreach(KeyValuePair<int, Rune> kvp in board)
        {
            List<int> explored = new List<int>();
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
    public bool IsConnectedToCenter(int position, ref List<int> explored, ref Dictionary<int, Rune> board)
    {
        if (position == 12)
        {
            return true;
        }

        if (explored == null)
        {
            explored = new List<int>();
        }

        explored.Add(position);
        List<int> positions = GetNeighboursPosition(position, board);

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
        foreach (KeyValuePair<int, Rune> kvp in _runesOnBoard)
        {
            elementsList.Add(kvp.Value.Element);
        }
        elementsList.Sort();

        Queue<Element> elementsQueue = new Queue<Element>(elementsList);

        return elementsQueue;
    }

	/// <summary>
	/// 
	/// </summary>
	/// <returns>A List of Element from runes that are on the board.</returns>
	public List<Element> GetSortedElementList()
	{
		List<Element> elementsList = new List<Element>();
		foreach (KeyValuePair<int, Rune> kvp in _runesOnBoard)
		{
			elementsList.Add(kvp.Value.Element);
		}
		elementsList.Sort();

		return elementsList;
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

    /// <summary>
    /// Calculates the percentage acquired from the influence of each pole on every rune.
    /// </summary>
    /// <param name="perfection">The perfection percentage.</param>
    /// <param name="sublimation">The sublimation percentage.</param>
    /// <param name="stability">The stability percentage.</param>
    public void GetPolesInfluence(out float perfection, out float sublimation, out float stability)
    {
        Hexagon hexPerfection = new Hexagon(_idPerfection / 5, _idPerfection % 5, null);
        Hexagon hexSublimation = new Hexagon(_idSublimation / 5, _idSublimation % 5, null);
        Hexagon hexStability = new Hexagon(_idStability / 5, _idStability % 5, null);

        float perfect = 0;
        float subli = 0;
        float stabi = 0;

        foreach(int id in _runesOnBoard.Keys)
        {
            Hexagon hexRune = new Hexagon(id / 5, id % 5, null);
            int distPerfection = hexRune.Distance(hexPerfection);
            int distSublimation = hexRune.Distance(hexSublimation);
            int distStability = hexRune.Distance(hexStability);

            perfect += Mathf.Ceil((4 - distPerfection) * 2.5f);
            subli += Mathf.Ceil((4 - distSublimation) * 2.5f);
            stabi += Mathf.Ceil((4 - distStability) * 2.5f);
        }

        perfection = perfect * 0.01f;
        sublimation = subli * 0.01f;
        stability = stabi * 0.01f;
    }

    /// <summary>
    /// Creates a list containing every unique link between two runes placed on the board.
    /// </summary>
    /// <returns></returns>
    public List<KeyValuePair<Element, Element>> GetRuneLinks()
    {
        Dictionary<int, KeyValuePair<Element, Element>> dict = new Dictionary<int, KeyValuePair<Element, Element>>();
        foreach(int pos in _runesOnBoard.Keys)
        {
            List<int> neighbours = GetNeighboursPosition(pos);
            foreach(int idNeigh in neighbours)
            {
                int idLink = pos + idNeigh;
                if (!dict.ContainsKey(idLink))
                {
                    KeyValuePair<Element, Element> link = new KeyValuePair<Element, Element>(_runesOnBoard[pos].Element, _runesOnBoard[idNeigh].Element);
                    dict.Add(idLink, link);
                }
            }
        }

        List<KeyValuePair<Element, Element>> listLink = new List<KeyValuePair<Element, Element>>();
        foreach(int id in dict.Keys)
        {
            listLink.Add(dict[id]);
        }

        return listLink;
    }
}
