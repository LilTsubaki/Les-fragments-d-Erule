using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunicBoard : MonoBehaviour {

    List<Rune> runesInHand;
    Dictionary<uint, Rune> runesOnBoard;

    public void Awake()
    {
        runesInHand = new List<Rune>();
        runesOnBoard = new Dictionary<uint, Rune>();
    }

    /// <summary>
    /// Put a rune on the board at this position.
    /// </summary>
    /// <param name="rune">The rune to place</param>
    /// <param name="position">The position where the rune will be placed</param>
    /// <returns>If the rune was succesfully placed</returns>
    private bool PlaceRuneOnBoard(Rune rune, uint position)
    {
        if (!runesOnBoard.ContainsKey(position) && runesInHand.Remove(rune))
        {
            runesOnBoard.Add(position, rune);
            rune.positionOnBoard = (int)position;
            return true;
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
