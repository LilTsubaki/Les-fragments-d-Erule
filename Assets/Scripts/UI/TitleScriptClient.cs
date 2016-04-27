using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using System.IO;

public class TitleScriptClient : MonoBehaviour
{
    public GameObject planks;
    public GameObject deckSelection;

    void Start()
    {
        AudioManager.GetInstance().Play("EruleVoice");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIManager.GetInstance().HidePanelNoStack("start");
            UIManager.GetInstance().ShowPanelNoStack("PanelRunicBoard");
            deckSelection.SetActive(true);
            planks.SetActive(true);
        }        
    }




}
