using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterUI : MonoBehaviour {

    public Character _character
    {
        get
        {
            if(ClientManager.GetInstance()._client != null)
                return ClientManager.GetInstance()._client.CurrentCharacter;

            return null;
        }
    }

    public Text _name;
    public Image _characterImage;
    public Slider _life;
    public Text _lifePointsText;
    public GameObject _actionPoints;
    public GameObject _movementPoints;
    public Image _point;

    public bool _isOnLeft;
    private List<Image> _listActionPoints;
    private List<Image> _listMovementPoints;

    //Resistances

    public Text _resFireText;
    public Text _resWaterText;
    public Text _resAirText;
    public Text _resEarthText;
    public Text _resWoodText;
    public Text _resMetalText;



    // Use this for initialization
    void Start () {

        _listActionPoints = new List<Image>();
        _listMovementPoints = new List<Image>();


        for (int i = 0; i < Character._maxActionPoints; ++i)
        {
            Image n = Instantiate(_point);
            n.transform.SetParent(_actionPoints.transform);
            n.enabled = true;
            n.gameObject.SetActive(true);
            Vector3 pos = n.gameObject.transform.position;
            if(_isOnLeft)
                pos.x = 10 + i * 30;
            else
                pos.x = -10 - i * 30;
            n.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);
            
            _listActionPoints.Add(n);
        }

        for (int i = 0; i < Character._maxMovementPoints; ++i)
        {
            Image n = Instantiate(_point);
            n.transform.SetParent(_movementPoints.transform);
            n.enabled = true;
            n.gameObject.SetActive(true);
            Vector3 pos = n.gameObject.transform.position;
            if (_isOnLeft)
                pos.x = 10 + i * 30;
            else
                pos.x = -10 - i * 30;
            n.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);

            _listMovementPoints.Add(n);
        }
    }

    // Update is called once per frame
    void Update () {
        if (_character != null)
        {
            UpdateLife();
            UpdateActionPoints();
            UpdateMovementPoints();
            UpdateResistances();
            UpdateTurn();
        }
	}

    void UpdateLife()
    {
        if (_character != null)
        {
            _life.value = _character._lifeCurrent;
            _life.maxValue = _character._lifeMax;
            _lifePointsText.text = _character._lifeCurrent.ToString();
        }
    }

    void UpdateActionPoints()
    {
        Color colorActive = new Color(0, 0.2f, 1, 1);
        Color colorEmpty = new Color(0.5f, 0.5f, 0.5f, 0.3f);

        if (_character != null)
        {
            int nbPoints = _character.CurrentActionPoints;
            for (int i = 0; i < _listActionPoints.Count; ++i)
            {
                Color c;
                if (i < nbPoints)
                {
                    c = colorActive;
                }
                else
                {
                    c = colorEmpty;
                }
                _listActionPoints[i].gameObject.GetComponent<Image>().color = c;
            }
        }
    }

    void UpdateMovementPoints()
    {
        Color colorActive = new Color(0, 1,0.2f, 1);
        Color colorEmpty = new Color(0.5f, 0.5f, 0.5f, 0.3f);

        if (_character != null)
        {
            int nbPoints = _character.CurrentMovementPoints;
            for (int i = 0; i < _listMovementPoints.Count; ++i)
            {
                Color c;
                if (i < nbPoints)
                {
                    c = colorActive;
                }
                else
                {
                    c = colorEmpty;
                }
                _listMovementPoints[i].gameObject.GetComponent<Image>().color = c;
            }
        }

    }

    void UpdateResistances()
    {
        if (_character != null)
        {
            int resFire = _character.GetElementResistance(Element.GetElement(0)) - _character.GetElementWeakness(Element.GetElement(0));
            int resWater = _character.GetElementResistance(Element.GetElement(1)) - _character.GetElementWeakness(Element.GetElement(1));
            int resAir = _character.GetElementResistance(Element.GetElement(2)) - _character.GetElementWeakness(Element.GetElement(2));
            int resEarth = _character.GetElementResistance(Element.GetElement(3)) - _character.GetElementWeakness(Element.GetElement(3));
            int resWood = _character.GetElementResistance(Element.GetElement(4)) - _character.GetElementWeakness(Element.GetElement(4));
            int resGlobal = _character.GetGlobalResistance() - _character.GetGlobalWeakness();

            _resFireText.text = resFire.ToString();
            _resWaterText.text = resWater.ToString();
            _resAirText.text = resAir.ToString();
            _resEarthText.text = resEarth.ToString();
            _resWoodText.text = resWood.ToString();
            _resMetalText.text = resGlobal.ToString();
        }
    }

    void UpdateTurn()
    {
        _name.text = _character.Name;
        if (ClientManager.GetInstance()._client.IsMyTurn)
        {
            _characterImage.color = Color.green;
        }
        else
        {
            _characterImage.color = Color.red;
        }
    }
}
