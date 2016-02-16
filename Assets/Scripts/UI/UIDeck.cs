using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIDeck : MonoBehaviour {

    public DeckSelectionBehaviour deck;

	public void SaveHandAndPlay()
    {
        if(deck.DeckSelection.RunesInHand.Count == 7)
        {
            DeckSelection ds = deck.DeckSelection;
            RunicBoardManager.GetInstance().TempHand = ds.RunesInHand;
            SceneManager.LoadScene(1);
        }
        else
        {
            Logger.Debug(deck.DeckSelection.RunesInHand.Count);
        }
    }
}
