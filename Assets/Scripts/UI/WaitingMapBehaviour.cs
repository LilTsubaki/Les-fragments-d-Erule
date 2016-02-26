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
            _runicBoard.SetActive(true);
        }
	}
}
