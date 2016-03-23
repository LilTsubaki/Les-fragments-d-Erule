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
    private bool _isInit = false;

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
        _isInit = true;
    }

    public void AddText(string text)
    {
        if (_isInit)
        {
            GameObject go;
            if (_texts.Count < _maxText)
            {
                go = GameObject.Instantiate<GameObject>(_textGO.transform.GetChild(0).gameObject);
                go.SetActive(true);
            
                go.AddComponent<Text>();
                _content.GetComponent<RectTransform>().sizeDelta += new Vector2(0,20);

                go.transform.SetParent(_textGO.transform);
            }
            else
            {
                go = _texts.Dequeue();
            }

            Text textComponent = go.GetComponent<Text>();
            textComponent.text = text;
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, _lastYPosition, 0);
            _texts.Enqueue(go);
            _textGO.transform.position -= new Vector3(0, 20, 0);
            _lastYPosition += 20;
            _content.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
