using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TestMaps : MonoBehaviour {

    public GameObject _buttonToCopy;
    public GameObject _content;

    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    public GameObject _buttonValidation;

    // Use this for initialization
    void Start () {
        GetFiles();
	}
	
	void Update () {
	
	}

    void GetFiles()
    {
        List<string> files = new List<string>();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/JsonFiles/Maps", "*.json");

        float offsetX = ((680/3.0f)-160)/2;
        for(int i = 0; i < fileEntries.Length; ++i)
        {
            string path = fileEntries[i];
            GameObject obj = Instantiate(_buttonToCopy);
            obj.SetActive(true);
            obj.transform.SetParent(_content.transform);
            obj.transform.localPosition = new Vector3(offsetX + (160 + offsetX*2) * (i%3), (i/3 + 1) * -250);
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { LoadMap(path); });
            button.GetComponentInChildren<Text>().text = Path.GetFileNameWithoutExtension(path);
        }
    }

    void LoadMap(string path)
    {
        GameObject o = new GameObject();
        string name = Path.GetFileNameWithoutExtension(path);
        TestSpawnNetwork tsn = o.AddComponent<TestSpawnNetwork>();
        tsn._button = _buttonValidation;
        tsn._button.GetComponent<Button>().onClick.AddListener(delegate { tsn.changeState(); });
        tsn._player1GameObject = _player1GameObject;
        tsn._player2GameObject = _player2GameObject;
        tsn._boardName = name;
        UIManager.GetInstance().HideAll();
        UIManager.GetInstance().ShowPanel("PanelPosition");
    }
}
