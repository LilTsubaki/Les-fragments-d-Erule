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

    public DeckSelection()
    {
        _runesInHand = new Dictionary<int, Rune>();
    }
    
    public bool PlaceRuneInHand(Rune rune, int position)
    {
        Rune runeToDelete;
        if (_runesInHand.TryGetValue(position, out runeToDelete))
            _runesInHand.Remove(position);
        
        rune.PositionInHand = position;
        _runesInHand.Add(position, rune);
        Logger.Debug("Rune placed at " + position);
        return true;
    }

    public bool RemoveRuneFromHand(int position)
    {
        return _runesInHand.Remove(position);
    }

    public bool ChangeRunePosition(int oldPosition, int newPosition)
    {
        if (oldPosition == newPosition)
        {
            return false;
        }

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
                return true;
            }
            else
            {
                Logger.Debug("Changed from " + oldPosition + " to " + newPosition);
                runeToMove.PositionInHand = newPosition;
                _runesInHand.Remove(oldPosition);
                _runesInHand.Add(newPosition, runeToMove);
                return true;
            }
        }
        else
        {
            Logger.Debug("No runes at " + oldPosition);
            return false;
        }
    }

}
