using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
         ___   
     ___/12 \___
 ___/ 7 \___/16 \___
/ 2 \___/11 \___/20 \
\___/ 6 \___/15 \___/
/ 1 \___/10 \___/19 \
\___/ 5 \___/14 \___/
/ 0 \___/ 9 \___/18 \
\___/ 4 \___/13 \___/
    \___/ 8 \___/
        \___/
*/

public class RunicBoard : MonoBehaviour {

    List<Rune> runesInHand;
    Dictionary<uint, Rune> runesOnBoard;

    public void Awake()
    {
        runesInHand = new List<Rune>();
        runesOnBoard = new Dictionary<uint, Rune>();
    }

    /// <summary>
    /// Verify if you can place a rune at the position, and place it if it is OK.
    /// </summary>
    /// <param name="rune">The rune to place</param>
    /// <param name="position">The position where the rune will be placed</param>
    /// <returns>If the rune was succesfully placed</returns>
    private bool PlaceRuneOnBoard(Rune rune, uint position)
    {
        // If no runes are on the board, places the rune in the center
        if (runesOnBoard.Count == 0)
        {
            runesOnBoard.Add(10, rune);
            return true;
        }

        // The player cannot place a rune if a rune is already in place at the position
        if (runesOnBoard.ContainsKey(position))
        {
            return false;
        }

        // If runes are placed around this position, place the rune
        List<uint> neighbours = GetNeighbours(position);
        for(int i = 0; i < neighbours.Count; i++)
        {
            if (runesOnBoard.ContainsKey(neighbours[i]))
            {
                runesOnBoard.Add(position, rune);
                rune.positionOnBoard = (int)position;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Remove a rune from the board at this position, and put in back in the hand.
    /// </summary>
    /// <param name="position">The position where the rune is expected to be</param>
    /// <returns>If the rune was succesfully removed</returns>
    private bool RemoveRuneFromBoard(uint position)
    {
        Rune rune;
        bool runeFound = runesOnBoard.TryGetValue(position, out rune);
        if (runeFound)
        {
            runesOnBoard.Remove(position);
            runesInHand.Add(rune);
            rune.positionOnBoard = -1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Remove all runes from the runic board and put them back in the hand.
    /// </summary>
    private void RemoveAllRunes()
    {
        foreach(KeyValuePair<uint, Rune> kvp in runesOnBoard)
        {
            Rune rune = kvp.Value;
            rune.positionOnBoard = -1;
            runesInHand.Add(rune);
        }
        runesOnBoard.Clear();
    }

    /// <summary>
    /// Check if the position exists on the board.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    bool PositionExists(uint i)
    {
        if (i != 3 && i >= 0 && i <= 20)
            return true;
        return false;
    }


    /// <summary>
    /// Get adjacent positions to the one in parameter.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public List<uint> GetNeighbours(uint position)
    {
        List<uint> neighbours = new List<uint>();

        uint n1 = position - 5;
        if (PositionExists(n1))
            neighbours.Add(n1);

        uint n2 = position - 4;
        if (PositionExists(n2))
            neighbours.Add(n2);

        uint n3 = position - 1;
        if (PositionExists(n3))
            neighbours.Add(n3);

        uint n4 = position + 1;
        if (PositionExists(n4))
            neighbours.Add(n4);

        uint n5 = position + 4;
        if (PositionExists(n5))
            neighbours.Add(n5);

        uint n6 = position - 5;
        if (PositionExists(n6))
            neighbours.Add(n6);

        return neighbours;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A List of Element from runes that are on the board.</returns>
    public List<Element> GetElements()
    {
        List<Element> elements = new List<Element>();
        foreach (KeyValuePair<uint, Rune> kvp in runesOnBoard)
        {
            elements.Add(kvp.Value.element);
        }

        return elements;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
