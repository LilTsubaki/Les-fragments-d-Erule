using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIDeck : MonoBehaviour {

    public DeckSelectionBehaviour _deck;
    public InputField _inputField;

	public void SaveHandAndPlay()
    {
        if(_deck.DeckSelection.RunesInHand.Count == 7)
        {
            DeckSelection ds = _deck.DeckSelection;
            RunicBoardManager.GetInstance().TempHand = ds.RunesInHand;
            if (_inputField.text.Length == 0)
            {
                _inputField.text = "No Name";
            }
            ClientManager.GetInstance()._client.Name = _inputField.text;

            UIManager.GetInstance().HidePanelNoStack("PanelRunicBoard");
            UIManager.GetInstance().ShowPanelNoStack("PanelServers");
            ClientManager.GetInstance()._client.SearchHost();
            _deck.gameObject.SetActive(false);
        }
        else
        {
            Logger.Debug("Not enough runes : " + _deck.DeckSelection.RunesInHand.Count);
        }
    }
}
