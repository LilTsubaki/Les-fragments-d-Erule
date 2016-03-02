﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoardManager
{
	private static PlayBoardManager _instance;
	private PlayBoard _board;

	public PlayBoard Board{
		get { return _board; }
        set { _board = value; }
	}

    public Character Character1
    {
        get
        {
            return _character1;
        }
    }

    public Character Character2
    {
        get
        {
            return _character2;
        }
    }

    private Character _character1;
	private Character _character2;

    private int _turnNumber;
    private bool _secondPlayerStarted;

    public enum State { MoveMode, SpellMode };
    private State _currentState;

    public static PlayBoardManager GetInstance(){
		if (_instance == null) {
			_instance = new PlayBoardManager ();
		}

		return _instance;
	}

    public int TurnNumber
    {
        get
        {
            return _turnNumber;
        }

        set
        {
            _turnNumber = value;
        }
    }

    public State CurrentState
    {
        get
        {
            return _currentState;
        }

        set
        {
            _currentState = value;
            switch (_currentState)
            {
                case State.MoveMode:
                    PlayBoardManager.GetInstance().Board._colorAccessible = true;
                    break;

                case State.SpellMode:

                    break;
            }
        }
    }

   

    private PlayBoardManager ()
	{
        _secondPlayerStarted = EruleRandom.RangeValue(0,100) % 2 == 0;
        _turnNumber = 0;
    }

    public void Init(int width, int height, Character character1, Character character2)
    {

        _board = new PlayBoard(width, height);
        _character1 = character1;
        _character2 = character2;
        EffectUIManager.GetInstance().RegisterCharacter(PlayBoardManager.GetInstance().Character1, 1.0f);
        EffectUIManager.GetInstance().RegisterCharacter(PlayBoardManager.GetInstance().Character2, 1.0f);
    }
        

    public void Init(PlayBoard pb, Character character1, Character character2)
    {
        _board = pb;
        _character1 = character1;
        _character2 = character2;
        EffectUIManager.GetInstance().RegisterCharacter(PlayBoardManager.GetInstance().Character1, 1.0f);
        EffectUIManager.GetInstance().RegisterCharacter(PlayBoardManager.GetInstance().Character2, 1.0f);
    }

    public List<Character> GetCharacterInArea(List<Hexagon> hexagons){
		List<Character> chars = new List<Character> ();

		foreach (var hex in hexagons) {
			if (Character1.Position.Equals (hex))
				chars.Add (Character1);
			if (Character2.Position.Equals (hex))
				chars.Add (Character2);
		}
		return chars;
	}

    public List<Killable> GetKillableInArea(List<Hexagon> hexagons)
    {
        List<Killable> killables = new List<Killable>();

        foreach (var hex in hexagons)
        {
            if(hex._entity!=null && hex._entity is Killable)
            {
                killables.Add((Killable)hex._entity);
            }
        }
        return killables;
    }

    public bool isMyTurn(Character charac)
    {
        if ((_turnNumber % 2 == 0 ^ _secondPlayerStarted) && Character1 == charac)
            return true;
        if ((_turnNumber % 2 == 1 ^ _secondPlayerStarted) && Character2 == charac)
            return true;
        return false;
    }

    public Character GetCurrentPlayer()
    {
        if (isMyTurn(Character1))
            return Character1;
        else
            return Character2;
    }

    public Character GetOtherPlayer()
    {
        if (isMyTurn(Character1))
            return Character2;
        else
            return Character1;
    }

    public void BeginTurn()
    {
        Character currentPlayer = GetCurrentPlayer();
        currentPlayer.RangeModifier = 0;
        currentPlayer.HealModifier = 0;
        currentPlayer.DamageModifier = 0;
        currentPlayer.IsStabilized = false;
        currentPlayer.GlobalProtectionModifier = 0;
        

        _board.ApplyPowerShards();

        currentPlayer.ApplyOnTimeEffects();
        currentPlayer.RemoveMarkedOnTimeEffects();

        List<List<Hexagon>> hexagons = Board.GetGrid();
        List<Hexagon> growableHexagons = new List<Hexagon>();

        for (int i = 0; i < hexagons.Count; ++i)
        {
            foreach (Hexagon hex in hexagons[i])
            {
                if (hex.ContainsGrowableEffect())
                    growableHexagons.Add(hex);
            }
        }

        foreach(Hexagon hex in growableHexagons)
        {
            hex.GrowUp();
        }

        for (int i = 0; i < hexagons.Count; ++i)
        {
            foreach (Hexagon hex in hexagons[i])
            {    
                hex.ApplyOnTimeEffects();
                hex.ReduceOnTimeEffectsCastedBy(currentPlayer);
                hex.RemoveMarkedOnTimeEffects();
            }
        }


        CurrentState = State.MoveMode;
    }

    public void EndTurn()
    {
        Character currentPlayer = GetCurrentPlayer();
        //Character otherPlayer = GetOtherPlayer();
        currentPlayer.RemoveMarkedOnTimeEffects();

        currentPlayer.TurnNumber++;
        currentPlayer.CurrentActionPoints = Math.Min(Character._maxActionPoints, 1 + currentPlayer.TurnNumber / 8);
        currentPlayer.CurrentMovementPoints = Math.Min(Character._maxMovementPoints, 1 + currentPlayer.TurnNumber / 8);

        _turnNumber++;
        Board._reset = true;
        _board.UpdatePowerShards();
        BeginTurn();
    }
}

