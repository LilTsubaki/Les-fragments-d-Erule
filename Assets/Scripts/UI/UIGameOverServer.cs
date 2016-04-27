using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameOverServer : MonoBehaviour {

    public Text text;

    bool _textSet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (ServerManager.GetInstance()._server.CurrentState == Server.State.gameOver && !_textSet)
        {
            text.text = PlayBoardManager.GetInstance().Winner.Name + " est victorieux !";
            _textSet = true;
        }
	}

    public void ShowCredits()
    {
        UIManager.GetInstance().ShowPanel("credits");
    }
}
