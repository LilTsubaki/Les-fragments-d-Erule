using UnityEngine;
using System.Collections;

public class WaitingMapBehaviour : MonoBehaviour {

    public GameObject _runicBoard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (ClientManager.GetInstance()._client.CurrentCharacter != null)
        {
            UIManager.GetInstance().HidePanelNoStack("PanelWaitingMap");
            UIManager.GetInstance().ShowPanelNoStack("PanelGame");
            UIManager.GetInstance().ShowPanelNoStack("menuButton");
            _runicBoard.SetActive(true);
        }
	}
}
