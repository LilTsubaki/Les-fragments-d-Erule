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
        UpdateLife();
        UpdateResistances();
	}

    void UpdateLife()
    {
        uint nbPoints = _nbPoints;
        //uint nbPoints = _character._currentActionPoints;
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

        int life = 2000;
        //int life = _character._lifeCurrent;
        _life.value = life;
    }

    void UpdateResistances()
    {

        float tileMaxSize = 0.02f * _resMask.sizeDelta.x/12;

        // Order Fire Water Earth Air Wood Metal
        int posFire = _resFire > 0 ? _resFire : 0;
        int posWater = _resWater > 0 ? _resWater : 0;
        int posEarth = _resEarth > 0 ? _resEarth : 0;
        int posAir = _resAir > 0 ? _resAir : 0;
        int posWood = _resWood > 0 ? _resWood : 0;
        int posMetal = _resMetal > 0 ? _resMetal : 0;

        _resPosFire.sizeDelta = new Vector2(posFire*tileMaxSize, _resPosFire.sizeDelta.y);
        _resPosWater.sizeDelta = new Vector2(posWater * tileMaxSize, _resPosWater.sizeDelta.y);
        _resPosEarth.sizeDelta = new Vector2(posEarth * tileMaxSize, _resPosEarth.sizeDelta.y);
        _resPosAir.sizeDelta = new Vector2(posAir * tileMaxSize, _resPosAir.sizeDelta.y);
        _resPosWood.sizeDelta = new Vector2(posWood * tileMaxSize, _resPosWood.sizeDelta.y);
        _resPosMetal.sizeDelta = new Vector2(posMetal * tileMaxSize, _resPosMetal.sizeDelta.y);

        int negFire = _resFire < 0 ? -_resFire : 0;
        int negWater = _resWater < 0 ? -_resWater : 0;
        int negEarth = _resEarth < 0 ? -_resEarth : 0;
        int negAir = _resAir < 0 ? -_resAir : 0;
        int negWood = _resWood < 0 ? -_resWood : 0;
        int negMetal = _resMetal < 0 ? -_resMetal : 0;

        _resNegFire.sizeDelta = new Vector2(negFire * tileMaxSize, _resNegFire.sizeDelta.y);
        _resNegWater.sizeDelta = new Vector2(negWater * tileMaxSize, _resNegWater.sizeDelta.y);
        _resNegEarth.sizeDelta = new Vector2(negEarth * tileMaxSize, _resNegEarth.sizeDelta.y);
        _resNegAir.sizeDelta = new Vector2(negAir * tileMaxSize, _resNegAir.sizeDelta.y);
        _resNegWood.sizeDelta = new Vector2(negWood * tileMaxSize, _resNegWood.sizeDelta.y);
        _resNegMetal.sizeDelta = new Vector2(negMetal * tileMaxSize, _resNegMetal.sizeDelta.y);

    }
}
