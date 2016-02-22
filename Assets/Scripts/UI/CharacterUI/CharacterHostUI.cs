﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterHostUI : MonoBehaviour
{

    private Character character;

    public Text _name;
    public Image _characterImage;
    public Slider _life;
    public Text _lifePointsText;
    public GameObject _actionPoints;
    public Image _point;


    /******************************************************************
    Testing parameters
    ******************************************************************/
    [Range(0, 4)]
    public int _nbPoints;

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

    public Text _resFireText;
    public Text _resWaterText;
    public Text _resAirText;
    public Text _resEarthText;
    public Text _resWoodText;
    public Text _resMetalText;

    public Character Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }



    // Use this for initialization
    void Start()
    {
        _listActionPoints = new List<Image>();

        for (int i = 0; i < Character._maxActionPoints; ++i)
        {
            Image n = Instantiate(_point);
            n.transform.SetParent(_actionPoints.transform);
            n.enabled = true;
            n.gameObject.SetActive(true);
            Vector3 pos = n.gameObject.transform.position;
            if (_isOnLeft)
                pos.x = 10 + i * 30;
            else
                pos.x = -10 - i * 30;
            n.rectTransform.anchoredPosition = new Vector2(pos.x, pos.y);

            _listActionPoints.Add(n);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Character != null)
        {
            UpdateLife();
            UpdateActionPoints();
            UpdateResistances();
            UpdateTurn();
        }
    }

    void UpdateLife()
    {
        if (Character != null)
        {
            _life.value = Character._lifeCurrent;
            _life.maxValue = Character._lifeMax;
            _lifePointsText.text = Character._lifeCurrent.ToString();
        }
    }

    void UpdateActionPoints()
    {
        Color colorActive = new Color(0, 0.2f, 1, 1);
        Color colorEmpty = new Color(0.5f, 0.5f, 0.5f, 0.3f);

        if (Character != null)
        {
            int nbPoints = Character.CurrentActionPoints;
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
        else
        {
            int nbPoints = _nbPoints;
            for (int i = 0; i < _listActionPoints.Count; ++i)
            {
                Color c = _listActionPoints[i].gameObject.GetComponent<Image>().color;
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

    void UpdateResistances()
    {

        // Order : Fire Water Air Earth Wood Metal

        int resFire = Character.GetElementResistance(Element.GetElement(0)) - Character.GetElementWeakness(Element.GetElement(0));
        int resWater = Character.GetElementResistance(Element.GetElement(1)) - Character.GetElementWeakness(Element.GetElement(1));
        int resAir = Character.GetElementResistance(Element.GetElement(2)) - Character.GetElementWeakness(Element.GetElement(2));
        int resEarth = Character.GetElementResistance(Element.GetElement(3)) - Character.GetElementWeakness(Element.GetElement(3));
        int resWood = Character.GetElementResistance(Element.GetElement(4)) - Character.GetElementWeakness(Element.GetElement(4));
        int resGlobal = Character.GetGlobalResistance() - Character.GetGlobalWeakness();

        _resFireText.text = resFire.ToString();
        _resWaterText.text = resWater.ToString();
        _resAirText.text = resAir.ToString();
        _resEarthText.text = resEarth.ToString();
        _resWoodText.text = resWood.ToString();
        _resMetalText.text = resGlobal.ToString();
    }

    void UpdateTurn()
    {
        _name.text = Character.Name;
        if (PlayBoardManager.GetInstance().isMyTurn(Character))
        {
            _characterImage.color = Color.green;
        }
        else
        {
            _characterImage.color = Color.red;
        }
    }
}