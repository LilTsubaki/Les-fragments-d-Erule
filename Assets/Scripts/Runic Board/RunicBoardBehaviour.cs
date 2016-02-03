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
        for (int i = 0; i < _board.RunesInHand.Count; i++)
        {
            Transform parent = _handGO.transform.GetChild(i);

            GameObject rune = new GameObject();
            
            switch(_board.RunesInHand[i]._element._name)
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
        List<Rune> hand = new List<Rune>();
        Rune r1 = new Rune(Element.GetElement(0), -1);
        hand.Add(r1);
        Rune r2 = new Rune(Element.GetElement(1), -1);
        hand.Add(r2);
        Rune r3 = new Rune(Element.GetElement(2), -1);
        hand.Add(r3);
        Rune r4 = new Rune(Element.GetElement(2), -1);
        hand.Add(r4);
        Rune r5 = new Rune(Element.GetElement(3), -1);
        hand.Add(r5);
        Rune r6 = new Rune(Element.GetElement(4), -1);
        hand.Add(r6);
        Rune r7 = new Rune(Element.GetElement(3), -1);
        hand.Add(r7);

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
