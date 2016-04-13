using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameOver : MonoBehaviour {
    bool _textSet = false;
    public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!_textSet)
        {
	        if (ClientManager.GetInstance()._client.GameOver && ClientManager.GetInstance()._client.Winner != null)
            {
                if(ClientManager.GetInstance()._client.Winner.Equals(ClientManager.GetInstance()._client.CurrentCharacter))
                {
                    text.text = "Vous avez gagné ! :)";
                }
                else
                {
                    text.text = "Vous avez perdu ! :(";
                }
                _textSet = true;
            }
        }
	}
}
