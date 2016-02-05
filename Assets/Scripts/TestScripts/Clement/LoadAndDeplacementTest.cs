﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadAndDeplacementTest : MonoBehaviour
{
    public string _boardName;
    public GameObject _player1GameObject;
    public GameObject _player2GameObject;

    private GameObject board;

    void Awake()
    {
        PlayBoard playBoard = JSONObject.JSONToBoard(ref board, _boardName);
        Hexagon hexaStart1 = playBoard.GetHexagone(0, 0);
        Hexagon hexaStart2 = playBoard.GetHexagone(6, 6);

        Character player1 = new Character(4000, hexaStart1, _player1GameObject);
        Character player2 = new Character(14298, hexaStart2, _player2GameObject);
        PlayBoardManager.GetInstance().Init(playBoard, player1, player2);



        Logger.logLvl = Logger.Type.TRACE;

        SpellManager.getInstance();
        Logger.Trace("spellManager initialized");

        Queue<Element> elements = new Queue<Element>();
        elements.Enqueue(Element.GetElement(3));
        elements.Enqueue(Element.GetElement(3));

        SelfSpell testSp = SpellManager.getInstance().ElementNode.GetSelfSpell(elements);
        Logger.Trace(testSp._effects.GetIds().Count);
        List<int> effectIds = testSp._effects.GetIds();

        List<Hexagon> rangeTest = new List<Hexagon>();
        for(int  i = 0; i < PlayBoardManager.GetInstance().Board._width; i++)
        {
            for (int j = 0; j < PlayBoardManager.GetInstance().Board._height; j++)
            {
                rangeTest.Add(PlayBoardManager.GetInstance().Board.GetHexagone(i, j));
            }
        }


        for (int i = 0; i < effectIds.Count; i++)
        {
            EffectDirect effectTest = SpellManager.getInstance().getDirectEffectById((uint)effectIds[i]);
            try
            {
                Heal heal = (Heal)effectTest;
                heal.ApplyEffect(rangeTest, hexaStart2, player1);
            }
            catch
            {
                try
                {
                    ProtectionElement protection = (ProtectionElement)effectTest;
                    protection.ApplyEffect(rangeTest, hexaStart2, player1);
                }
                catch
                {
                    Logger.Error("je ne suis pas un directEffect aeuaeuaueuaeua");
                } 
            }            
        }
    }

	// Use this for initialization
	void Start ()
    {
	
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void EndOfTurn()
    {
        TurnManager.GetInstance().EndTurn();
    }
}