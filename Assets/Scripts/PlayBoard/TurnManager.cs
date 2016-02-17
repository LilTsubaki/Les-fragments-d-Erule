using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TurnManager
{
    private static TurnManager _turnManager;

    private int _turnNumber;
    private bool _secondPlayerStarted;

    public enum State { MoveMod, SpellMod};

    private State _state;

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

    public State _State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
            switch (_state)
            {
                case State.MoveMod:
                    PlayBoardManager.GetInstance().Board._colorAccessible = true;
                    break;

                case State.SpellMod:

                    break;
            }
        }
    }

    private TurnManager()
    {
        _secondPlayerStarted = new Random().Next(100) % 2 == 0;
        _turnNumber = 0;
    }

    public static TurnManager GetInstance()
    {
        if (_turnManager == null)
        {
            _turnManager = new TurnManager();
        }
        return _turnManager;
    }

    public bool isMyTurn(Character charac)
    {
        if((_turnNumber % 2 == 0 ^ _secondPlayerStarted) && PlayBoardManager.GetInstance().Character1 == charac)
            return true;
        if ((_turnNumber % 2 == 1 ^ _secondPlayerStarted) && PlayBoardManager.GetInstance().Character2 == charac)
            return true;
        return false;
    }

    public void BeginTurn()
    {

        TurnManager.GetInstance()._State = TurnManager.State.MoveMod;

        Character currentPlayer = PlayBoardManager.GetInstance().GetCurrentPlayer();
        currentPlayer.ApplyOnTimeEffects();
        currentPlayer.RemoveMarkedOnTimeEffects();
        
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
        Character currentPlayer = PlayBoardManager.GetInstance().GetCurrentPlayer();
        Character otherPlayer = PlayBoardManager.GetInstance().GetOtherPlayer();
        currentPlayer.ApplyOnTimeEffects();
        currentPlayer.RemoveMarkedOnTimeEffects();

        currentPlayer.TurnNumber++;
        currentPlayer.CurrentActionPoints = Math.Min(Character._maxActionPoints, 1 + currentPlayer.TurnNumber / 5);

        _turnNumber++;
        PlayBoardManager.GetInstance().Board._reset = true;
        BeginTurn();


        //penser à appliquer les effets des buffs/debuffs sur les joeuurs
        //+ reset ce qu'il y a à reset
    }
}
