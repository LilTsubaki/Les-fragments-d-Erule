using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunicBoardBehaviour : MonoBehaviour {
    private RunicBoard _board;

    public GameObject _handGO;
    public GameObject _boardGO;

    public GameObject _airRuneAsset;
    public GameObject _earthRuneAsset;
    public GameObject _fireRuneAsset;
    public GameObject _metalRuneAsset;
    public GameObject _woodRuneAsset;
    public GameObject _waterRuneAsset;

    private void DisplayRunesInHand()
    {
        foreach(KeyValuePair<uint, Rune> kvp in _board.RunesInHand)
        {
            Transform parent = _handGO.transform.GetChild((int)kvp.Key);
            GameObject rune = new GameObject();

            switch (kvp.Value.Element._name)
            {
                case "Fire":
                    rune = GameObject.Instantiate(_fireRuneAsset);
                    break;
                case "Water":
                    rune = GameObject.Instantiate(_waterRuneAsset);
                    break;
                case "Air":
                    rune = GameObject.Instantiate(_airRuneAsset);
                    break;
                case "Earth":
                    rune = GameObject.Instantiate(_earthRuneAsset);
                    break;
                case "Wood":
                    rune = GameObject.Instantiate(_woodRuneAsset);
                    break;
                case "Metal":
                    rune = GameObject.Instantiate(_metalRuneAsset);
                    break;
                default:
                    break;
            }

            rune.transform.SetParent(parent);
            rune.transform.localPosition = new Vector3(0, 0.3f, 0);
            rune.transform.Rotate(new Vector3(0, 1, 0), 90);
        }
    }

    void InputUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void Awake()
    {
        Dictionary<uint, Rune> hand = new Dictionary<uint, Rune>();
        Rune r1 = new Rune(Element.GetElement(0), -1, 0);
        hand.Add(r1.PositionInHand, r1);
        Rune r2 = new Rune(Element.GetElement(1), -1, 1);
        hand.Add(r2.PositionInHand, r2);
        Rune r3 = new Rune(Element.GetElement(2), -1, 2);
        hand.Add(r3.PositionInHand, r3);
        Rune r4 = new Rune(Element.GetElement(3), -1, 3);
        hand.Add(r4.PositionInHand, r4);
        Rune r5 = new Rune(Element.GetElement(4), -1, 4);
        hand.Add(r5.PositionInHand, r5);
        Rune r6 = new Rune(Element.GetElement(5), -1, 5);
        hand.Add(r6.PositionInHand, r6);

        _board = new RunicBoard(hand);

        DisplayRunesInHand();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
