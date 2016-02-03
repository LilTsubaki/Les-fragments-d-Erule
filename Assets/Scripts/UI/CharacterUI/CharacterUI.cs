using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterUI : MonoBehaviour {

    Character _character;

    public Text _name;
    public Image _characterImage;
    public Slider _life;
    public GameObject _actionPoints;
    public Image _point;

    [Range(0,7)]
    public uint _nbPoints;

    private List<Image> _listActionPoints;

	// Use this for initialization
	void Start () {
        _name.text = "Xx-S0rc310r-xX";
        //_life.maxValue = _character._lifeMax;
        _life.maxValue = 3000;
        _life.value = _life.maxValue;

        _listActionPoints = new List<Image>();

        for (int i = 0; i < 7; ++i)
        {
            Image n = Instantiate(_point);
            n.transform.SetParent(_actionPoints.transform);
            n.enabled = true;
            Vector3 pos = n.gameObject.transform.position;
            pos.x = 10 + i * (20 + 5);
            n.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);
            
            _listActionPoints.Add(n);
        }
        Debug.Log(_listActionPoints.Count);
    }

    // Update is called once per frame
    void Update () {
        uint nbPoints = _nbPoints;
        //uint nbPoints = _character._currentActionPoints;
        for(int i = 0; i < _listActionPoints.Count; ++i)
        {
            if(i < nbPoints)
            {
                _listActionPoints[i].gameObject.SetActive(true);
            }
            else
            {
                _listActionPoints[i].gameObject.SetActive(false);
            }
        }

        int life = 2000;
        //int life = _character._lifeCurrent;
        _life.value = life;
        
	}
}
