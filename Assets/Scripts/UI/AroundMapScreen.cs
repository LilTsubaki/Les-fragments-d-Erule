using UnityEngine;
using System.Collections;

public class AroundMapScreen : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        PlayBoard pb = PlayBoardManager.GetInstance().Board;
        if (pb != null)
            CameraManager.GetInstance().AroundY(pb._center, .1f);

        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HideAll();
            UIManager.GetInstance().ShowPanelNoStack("PanelPosition");
            CameraManager.GetInstance().FadeTo("cameraBoard", 1);
        }
	}
}
