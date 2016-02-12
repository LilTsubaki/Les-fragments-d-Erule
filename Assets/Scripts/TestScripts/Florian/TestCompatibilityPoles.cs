﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TestCompatibilityPoles : MonoBehaviour
{
    public GameObject _runicBoard;
    public string _boardName;
    public GameObject _player1GameObject;
    public GameObject _player2GameObject;

    public CharacterUI _uiPlayer1;
    public CharacterUI _uiPlayer2;

    private GameObject board;

    PlayBoard playBoard;
    Hexagon hexaStart1;
    Hexagon hexaStart2;

    Character player1;
    Character player2;
    List<Hexagon> rangeTest;

    void Awake()
    {
        playBoard = JSONObject.JSONToBoard(ref board, _boardName);
        hexaStart1 = playBoard.GetHexagone(8, 8);
        hexaStart2 = playBoard.GetHexagone(12, 12);

        player1 = new Character(4000, hexaStart1, _player1GameObject);
        player2 = new Character(4000, hexaStart2, _player2GameObject);
        PlayBoardManager.GetInstance().Init(playBoard, player1, player2);

        player1.Name = "Player 1";
        _uiPlayer1.SetCharacter(player1);
        player2.Name = "Player 2";
        _uiPlayer2.SetCharacter(player2);

        rangeTest = new List<Hexagon>();
        rangeTest.Add(hexaStart1);

        Logger.logLvl = Logger.Type.TRACE;

        SpellManager.getInstance();
        Logger.Trace("spellManager initialized");

        /*Queue<Element> elements = new Queue<Element>();
        elements.Enqueue(Element.GetElement(3));
        elements.Enqueue(Element.GetElement(3));

        SelfSpell testSp = SpellManager.getInstance().ElementNode.GetSelfSpell(elements);
        Logger.Trace(testSp._effects.GetIds().Count);
        List<int> effectIds = testSp._effects.GetIds();

        
        /*for(int  i = 0; i < PlayBoardManager.GetInstance().Board._width; i++)
        {
            for (int j = 0; j < PlayBoardManager.GetInstance().Board._height; j++)
            {
                rangeTest.Add(PlayBoardManager.GetInstance().Board.GetHexagone(i, j));
            }
        }*/


        /*for (int i = 0; i < effectIds.Count; i++)
        {
            EffectDirect effectTest = SpellManager.getInstance().getDirectEffectById((int)effectIds[i]);
            effectTest.ApplyEffect(rangeTest, hexaStart1, player1);
        }*/
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
        _runicBoard.GetComponent<RunicBoardBehaviour>().ResetRunes();
    }

    public void tryingToDoSpell()
    {
        if(PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
        {
            /*List<Element> elementsList = _runicBoard.GetComponent<RunicBoardBehaviour>().Board.GetSortedElementList();
            Queue<Element> elements = new Queue<Element>(elementsList);
            SpellManager.getInstance().InitSpell(elements);*/

            float baseChance = RunicBoardManager.GetInstance().GetBaseStabilityByRuneNumber();
            float coef = RunicBoardManager.GetInstance().GetReductionCoefficientByRuneNumber();
            float malus = RunicBoardManager.GetInstance().GetTotalCompatibilityMalus();
            float perf, subli, stab;
            RunicBoardManager.GetInstance().GetBoardPlayer1().GetPolesInfluence(out perf, out subli, out stab);

            Logger.Debug("Chances to success : " + baseChance + " - (" + malus + " / " + coef + "). Poles : perfection " + perf + " / sublimation " + subli + " / stab " + stab);
            float totalStability = (baseChance - (malus / coef))*0.01f + stab;
            Logger.Debug("Total [ Chances :" + totalStability + ", Sublimation : " + subli + ", Perfection : " + perf + "]");

        }
    }
}