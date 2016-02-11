using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITurn : MonoBehaviour {

    public Text _turnText;
    public Text _playerName;
	
	// Update is called once per frame
	void Update () {
        Character c = PlayBoardManager.GetInstance().GetCurrentPlayer();
        _turnText.text = c.TurnNumber.ToString();
        _playerName.text = c.Name;
	}
}
