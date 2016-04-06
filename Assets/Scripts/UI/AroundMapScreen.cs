using UnityEngine;
using System.Collections;

public class AroundMapScreen : MonoBehaviour {

    public SpawnAndGameBehaviour map;
	
	// Update is called once per frame
	void Update () {

        PlayBoard pb = PlayBoardManager.GetInstance().Board;
        /*if (pb != null)
            CameraManager.GetInstance().AroundY(pb._center, .1f);*/

        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HideAll();
            UIManager.GetInstance().ShowPanelNoStack("PanelPosition");
            UIManager.GetInstance().ShowPanelNoStack("menuButton");
            CameraManager.GetInstance().FadeTo("cameraBoard", 1);
			//CameraManager.GetInstance().Active.GetComponent<Animator>().SetTrigger("ExitLoop");
			Invoke ("FadeToMovingBackCamera", 0.5f);
            map.changeState();
        }
	}

	void FadeToMovingBackCamera(){
		CameraManager.GetInstance().Active.GetComponent<Animator>().SetTrigger("ExitLoop");
	}
}
