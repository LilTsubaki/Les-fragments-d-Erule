using UnityEngine;
using System.Collections;

public class TitleScreenClientBehaviour : MonoBehaviour {

    public GameObject _deckSelectionGo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HidePanelNoStack("PanelTitleScreen");
            UIManager.GetInstance().ShowPanelNoStack("PanelRunicBoard");
            _deckSelectionGo.SetActive(true);
        }
    }
}
