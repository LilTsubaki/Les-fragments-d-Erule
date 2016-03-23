using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HistoricManager {

    private static HistoricManager _instance;
    private GameObject _textGO;
    private GameObject _content;
    private int _maxText;
    private Queue<GameObject> _texts;
    private int _lastYPosition = 0;

    HistoricManager()
    {
        _texts = new Queue<GameObject>();
    }

    public static HistoricManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new HistoricManager();
        }
        return _instance;
    }

    public void Init(GameObject content, GameObject textGO, int maxText)
    {
        _content = content;
        _textGO = textGO;
        _maxText = maxText;
    }

    public void AddText(string text)
    {
        if (_texts.Count < _maxText)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(_textGO.transform);
            go.transform.position = new Vector3(0, _lastYPosition, 0);
            Text textComponent = go.AddComponent<Text>();
            textComponent.text = text;
            _texts.Enqueue(go);
            _content.GetComponent<RectTransform>().sizeDelta += new Vector2(0,20);
        }
        else
        {
            GameObject go = _texts.Dequeue();
            go.transform.SetParent(_textGO.transform);
            go.transform.position = new Vector3(0, _lastYPosition, 0);
            _textGO.transform.position -= new Vector3(0, 20, 0);
            Text textComponent = go.GetComponent<Text>();
            textComponent.text = text;
            _texts.Enqueue(go);
        }

        _lastYPosition += 20;

    }
}
