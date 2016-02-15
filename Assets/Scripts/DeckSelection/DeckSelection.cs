using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeckSelection {

    private Dictionary<int, Rune> _runesInHand;

    public Dictionary<int, Rune> RunesInHand
    {
        get
        {
            return _runesInHand;
        }
    }
    
    public void placeRuneInHand(Rune rune, int position)
    {
        rune.PositionInHand = position;
        _runesInHand.Add(position, rune);
    }

    public void removeRuneFromHand(int position)
    {
        _runesInHand.Remove(position);
    }

    public void changeRunePosition(int oldPosition, int newPosition)
    {
        Rune runeToMove;
        if (_runesInHand.TryGetValue(oldPosition, out runeToMove))
        {
            Rune runeToReplace;
            if (_runesInHand.TryGetValue(newPosition, out runeToReplace))
            {
                Logger.Debug("Swapping " + oldPosition + " and " + newPosition);
                runeToMove.PositionInHand = newPosition;
                runeToReplace.PositionInHand = oldPosition;

                _runesInHand.Remove(oldPosition);
                _runesInHand.Remove(newPosition);

                _runesInHand.Add(newPosition, runeToMove);
                _runesInHand.Add(oldPosition, runeToReplace);
            }
            else
            {
                Logger.Debug("Changed from " + oldPosition + " to " + newPosition);
                _runesInHand.Remove(oldPosition);
                _runesInHand.Add(newPosition, runeToMove);
            }
        }
        else
        {
            Logger.Debug("No runes at " + oldPosition);
        }
    }

}
