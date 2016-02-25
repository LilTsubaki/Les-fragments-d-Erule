using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyBehaviour : MonoBehaviour {

    public Text _textPlayer1;
    public Text _textPlayer2;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
        if(ServerManager.GetInstance()._server.Client1 != null)
        {
            _textPlayer1.text = "- " + ServerManager.GetInstance()._server.Client1._name + " connecté !";
        }

        if(ServerManager.GetInstance()._server.Client2 != null)
        {
            _textPlayer1.text = "- " + ServerManager.GetInstance()._server.Client2._name + " connecté !";
        }
    }
}
