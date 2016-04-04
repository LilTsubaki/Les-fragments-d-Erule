using UnityEngine;
using System.Collections;

public class TitleScreenServerBehaviour : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HidePanelNoStack("PanelTitleScreen");
            UIManager.GetInstance().ShowPanelNoStack("PanelLobby");
            UIManager.GetInstance().HidePanelNoStack("menuButton");
            UIManager.GetInstance().ShowPanelNoStack("menuButton");
        }
	}
}
