using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterUI : MonoBehaviour {

    CharacterBehaviour _characterBehaviour;

    public Text _name;
    public Image _characterImage;
    public Slider _life;
    public GameObject _actionPoints;
    public Image _point;


    /******************************************************************
    Testing parameters
    ******************************************************************/
    [Range(0,7)]
    public uint _nbPoints;

    [Range(-50, 50)]
    public int _resFire;
    [Range(-50, 50)]
    public int _resWater;
    [Range(-50, 50)]
    public int _resEarth;
    [Range(-50, 50)]
    public int _resAir;
    [Range(-50, 50)]
    public int _resWood;
    [Range(-50, 50)]
    public int _resMetal;


    /*******************************************************************
    End testing parameters
    *******************************************************************/

    public bool _isOnLeft;
    private List<Image> _listActionPoints;

    //Resistances

    public RectTransform _resMask;

    public RectTransform _resPosFire;
    public RectTransform _resPosWater;
    public RectTransform _resPosEarth;
    public RectTransform _resPosAir;
    public RectTransform _resPosWood;
    public RectTransform _resPosMetal;

    public RectTransform _resNegFire;
    public RectTransform _resNegWater;
    public RectTransform _resNegEarth;
    public RectTransform _resNegAir;
    public RectTransform _resNegWood;
    public RectTransform _resNegMetal;

    // Use this for initialization
    void Start () {
        _name.text = "Xx-S0rc310r-xX";
        //_life.maxValue = _character._lifeMax;
        _life.maxValue = _characterBehaviour._character._lifeMax;
        _life.value = _characterBehaviour._character._lifeMax;

        _listActionPoints = new List<Image>();

        for (int i = 0; i < 7; ++i)
        {
            Image n = Instantiate(_point);
            n.transform.SetParent(_actionPoints.transform);
            n.enabled = true;
            Vector3 pos = n.gameObject.transform.position;
            if(_isOnLeft)
                pos.x = 10 + i * (20 + 5);
            else
                pos.x = -10 - i * (20 + 5);
            n.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);
            
            _listActionPoints.Add(n);
        }
    }

    // Update is called once per frame
    void Update () {
        UpdateLife();
        UpdateResistances();
	}

    void UpdateLife()
    {
        uint nbPoints = _characterBehaviour._character._currentActionPoints;
        for (int i = 0; i < _listActionPoints.Count; ++i)
        {
            if (i < nbPoints)
            {
                _listActionPoints[i].gameObject.SetActive(true);
            }
            else
            {
                _listActionPoints[i].gameObject.SetActive(false);
            }
        }

        uint life = _characterBehaviour._character._lifeCurrent;
        _life.value = life;
    }

    void UpdateResistances()
    {
        float midSize = _resMask.sizeDelta.x / 2 / Character.MaxProtection;

        // Order : Fire Water Air Earth Wood Metal
        /*int posFire = _resFire > 0 ? _resFire : 0;
        int posWater = _resWater > 0 ? _resWater : 0;
        int posEarth = _resEarth > 0 ? _resEarth : 0;
        int posAir = _resAir > 0 ? _resAir : 0;
        int posWood = _resWood > 0 ? _resWood : 0;
        int posMetal = _resMetal > 0 ? _resMetal : 0;*/

        Character chara = _characterBehaviour._character;

        uint posFire = chara.GetElementResistance(Element.GetElement(0));
        uint posWater = chara.GetElementResistance(Element.GetElement(1));
        uint posAir = chara.GetElementResistance(Element.GetElement(2));
        uint posEarth = chara.GetElementResistance(Element.GetElement(3));
        uint posWood = chara.GetElementResistance(Element.GetElement(4));
        uint posMetal = chara.GetGlobalResistance();

        _resPosFire.sizeDelta = new Vector2(posFire*midSize, _resPosFire.sizeDelta.y);
        _resPosWater.sizeDelta = new Vector2(posWater * midSize, _resPosWater.sizeDelta.y);
        _resPosAir.sizeDelta = new Vector2(posAir * midSize, _resPosAir.sizeDelta.y);
        _resPosEarth.sizeDelta = new Vector2(posEarth * midSize, _resPosEarth.sizeDelta.y);
        _resPosWood.sizeDelta = new Vector2(posWood * midSize, _resPosWood.sizeDelta.y);
        _resPosMetal.sizeDelta = new Vector2(posMetal * midSize, _resPosMetal.sizeDelta.y);

        uint negFire = chara.GetElementWeakness(Element.GetElement(0));
        uint negWater = chara.GetElementWeakness(Element.GetElement(1));
        uint negAir = chara.GetElementWeakness(Element.GetElement(2));
        uint negEarth = chara.GetElementWeakness(Element.GetElement(3));
        uint negWood = chara.GetElementWeakness(Element.GetElement(4));
        uint negMetal = chara.GetGlobalWeakness();

        _resNegFire.sizeDelta = new Vector2(negFire * midSize, _resNegFire.sizeDelta.y);
        _resNegWater.sizeDelta = new Vector2(negWater * midSize, _resNegWater.sizeDelta.y);
        _resNegAir.sizeDelta = new Vector2(negAir * midSize, _resNegAir.sizeDelta.y);
        _resNegEarth.sizeDelta = new Vector2(negEarth * midSize, _resNegEarth.sizeDelta.y);
        _resNegWood.sizeDelta = new Vector2(negWood * midSize, _resNegWood.sizeDelta.y);
        _resNegMetal.sizeDelta = new Vector2(negMetal * midSize, _resNegMetal.sizeDelta.y);

    }
}
