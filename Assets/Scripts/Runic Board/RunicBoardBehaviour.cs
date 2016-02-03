using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunicBoardBehaviour : MonoBehaviour {
    private RunicBoard _board;

    public GameObject FireRuneAsset;
    public GameObject WaterRuneAsset;
    public GameObject AirRuneAsset;
    public GameObject EarthRuneAsset;
    public GameObject WoodRuneAsset;
    public GameObject MetalRuneAsset;

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
        Rune r6 = new Rune(Element.GetElement(3), -1);
        hand.Add(r6);

        _board = new RunicBoard(hand);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
