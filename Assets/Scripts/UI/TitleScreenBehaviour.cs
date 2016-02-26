﻿using UnityEngine;
using System.Collections;

public class TitleScreenBehaviour : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HidePanelNoStack("PanelTitleScreen");
            UIManager.GetInstance().ShowPanelNoStack("PanelLobby");
        }
	}
}