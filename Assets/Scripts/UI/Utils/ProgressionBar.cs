using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressionBar : MonoBehaviour {

    RectTransform _rect;
    public Image _valueBar;
    public float _minValue;
    public float _maxValue;
    public float _value;

	// Use this for initialization
	void Start () {
        _rect = gameObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 size = _rect.sizeDelta;
        float percentage;
        if (_maxValue == _minValue)
        {
            percentage = 1;
        }
        else
        {
            percentage = Mathf.Clamp01((_value - _minValue) / (_maxValue - _minValue));
        }
        Vector2 newSize = new Vector2(size.x * percentage, 0);
        _valueBar.GetComponent<RectTransform>().sizeDelta = newSize;
	}
}
