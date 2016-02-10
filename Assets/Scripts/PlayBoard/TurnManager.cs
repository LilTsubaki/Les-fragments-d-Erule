﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TurnManager
{
    private static TurnManager _turnManager;

    private int _turnNumber;
    private bool _firstPlayer;

    private TurnManager()
    {
        _firstPlayer = new Random().Next(100) % 2 == 0;
        _turnNumber = 0;
    }

    public static TurnManager GetInstance()
    {
        if (_turnManager == null)
            _turnManager = new TurnManager();
        return _turnManager;
    }

    public bool isMyTurn(Character charac)
    {
        if((_turnNumber%2 == 0 ^ _firstPlayer) && PlayBoardManager.GetInstance().Character1 == charac)
            return true;
        if ((_turnNumber % 2 == 1 ^ _firstPlayer) && PlayBoardManager.GetInstance().Character2 == charac)
            return true;
        return false;
    }

    public void BeginTurn()
    {
        PlayBoardManager.GetInstance().GetCurrentPlayer().ApplyOnTimeEffects();
        PlayBoardManager.GetInstance().GetCurrentPlayer().RemoveMarkedOnTimeEffects();
        List<List<Hexagon>> hexagons = PlayBoardManager.GetInstance().Board.GetGrid();
        for(int i = 0; i < hexagons.Count; ++i)
        {
            foreach(Hexagon hex in hexagons[i])
            {
                hex.ApplyOnTimeEffects();
                hex.RemoveMarkedOnTimeEffects();
            }
        }

    }

    public void EndTurn()
    {
        _turnNumber++;
        PlayBoardManager.GetInstance().Board.ResetBoard();

        BeginTurn();
        //penser à appliquer les effets des buffs/debuffs sur les joeuurs
        //+ reset ce qu'il y a à reset
    }
}
