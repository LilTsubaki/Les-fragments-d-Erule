using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadMaps : MonoBehaviour
{

    public GameObject _buttonToCopy;
    public GameObject _content;

    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    public GameObject _buttonValidation;

    public CharacterHostUI _uiPlayer1;
    public CharacterHostUI _uiPlayer2;

    private string _chosenMap;
    private string _chosenEnvironment;

    public AroundMapScreen _around;
    public Text _textPlayerToPlace;

    // Use this for initialization
    void Start()
    {
        GetFiles();
    }

    void Update()
    {

    }

    void GetFiles()
    {
        int nbButtonsPerLine = 1;
        float buttonSizeX = _buttonToCopy.GetComponent<RectTransform>().sizeDelta.x;
        float buttonSizeY = _buttonToCopy.GetComponent<RectTransform>().sizeDelta.y;

        JSONObject maps = JSONObject.GetJsonObjectFromFile("JsonFiles/Maps/MapList");
        List<JSONObject> maplist = maps.GetField("maps").list;
        

        _content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1.0f * ((maplist.Count) / nbButtonsPerLine) * buttonSizeY);
        _content.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

        float offsetX = ((680 / (float)nbButtonsPerLine) - buttonSizeX) / 2;

        for (int i = 0; i < maplist.Count; ++i)
        {
            string path = maplist[i].GetField("path").str;
            string mapName = maplist[i].GetField("name").str;
            string mapImage = maplist[i].GetField("miniature").str;
            string environment = maplist[i].GetField("environment").str;


            GameObject obj = Instantiate(_buttonToCopy);
            obj.SetActive(true);
            obj.transform.SetParent(_content.transform);
            obj.transform.localPosition = new Vector3(offsetX + (buttonSizeX + offsetX * 2) * (i % nbButtonsPerLine), (i / nbButtonsPerLine + 1) * -buttonSizeY);

            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { LoadingScreen(path, environment); });
            button.GetComponentInChildren<Text>().text = mapName;
            Sprite image = Resources.Load("miniatures/" + mapImage, typeof(Sprite)) as Sprite;
            if (image == null)
            {
                Logger.Error("Not found " + mapName);
            }
            else
            {
                obj.GetComponent<Image>().sprite = image;
            }
        }
    }

    void LoadingScreen(string path, string environment)
    {
        UIManager.GetInstance().FadeOutPanelNoStack("PanelChoiceMap");
        UIManager.GetInstance().FadeInPanelNoStack("Loading");
        _chosenMap = path;
        _chosenEnvironment = environment;
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
        tsn._prefabEnvironmentName = _chosenEnvironment;
        _around.map = tsn;
        tsn._textPlayerToPlace = _textPlayerToPlace;
    }
}
