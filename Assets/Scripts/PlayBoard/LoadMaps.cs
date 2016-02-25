using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadMaps : MonoBehaviour {

    public GameObject _buttonToCopy;
    public GameObject _imageToCopy;
    public GameObject _content;

    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    public GameObject _buttonValidation;

    public CharacterHostUI _uiPlayer1;
    public CharacterHostUI _uiPlayer2;

    private string _chosenMap;

    // Use this for initialization
    void Start () {
        GetFiles();
	}
	
	void Update () {
	
	}

    void GetFiles()
    {
        int nbButtonsPerLine = 1;
        float buttonSizeX = _buttonToCopy.GetComponent<RectTransform>().sizeDelta.x;
        float buttonSizeY = _buttonToCopy.GetComponent<RectTransform>().sizeDelta.y;

        List<string> files = new List<string>();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/JsonFiles/Maps", "*.json");
        _content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1.0f*((fileEntries.Length) / nbButtonsPerLine) * buttonSizeY);
        _content.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        float offsetX = ((680/(float)nbButtonsPerLine) - buttonSizeX) /2;
        for(int i = 0; i < fileEntries.Length; ++i)
        {
            string path = fileEntries[i];
            GameObject obj = Instantiate(_buttonToCopy);
            obj.SetActive(true);
            obj.transform.SetParent(_content.transform);
            obj.transform.localPosition = new Vector3(offsetX + (buttonSizeX + offsetX*2) * (i% nbButtonsPerLine), (i/ nbButtonsPerLine + 1) * -buttonSizeY);
            
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { LoadingScreen(path); });
            string mapName = Path.GetFileNameWithoutExtension(path);
            button.GetComponentInChildren<Text>().text = mapName;
            Sprite image = Resources.Load("miniatures/" + mapName, typeof(Sprite)) as Sprite;
            if (image == null)
            {
                Logger.Error("Not found " + mapName);
            }
            else
            {
                obj.GetComponent<Image>().sprite = image;
            }
        }
        Logger.Debug("Boop");
    }

    void LoadingScreen(string path)
    {
        UIManager.GetInstance().HideAll();
        UIManager.GetInstance().FadeInPanelNoStack("Loading");
        _chosenMap = path;
        Invoke("LoadMap", 1);
    }

    void LoadMap()
    {
        string path = _chosenMap;
        GameObject o = new GameObject();
        string name = Path.GetFileNameWithoutExtension(path);
        TestSpawnNetwork tsn = o.AddComponent<TestSpawnNetwork>();
        tsn._button = _buttonValidation;
        tsn._button.GetComponent<Button>().onClick.AddListener(delegate { tsn.changeState(); });
        tsn._player1GameObject = _player1GameObject;
        tsn._player2GameObject = _player2GameObject;
        tsn._uiPlayer1 = _uiPlayer1;
        tsn._uiPlayer2 = _uiPlayer2;
        tsn._boardName = name;
    }
}
