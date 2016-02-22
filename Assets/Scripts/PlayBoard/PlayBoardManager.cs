using System;
using System.Collections.Generic;

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
        _secondPlayerStarted = new Random().Next(100) % 2 == 0;
        _turnNumber = 0;
    }

    public void Init(int width, int height, Character character1, Character character2)
    {

        _board = new PlayBoard(width, height);
        _character1 = character1;
        _character2 = character2;
    }
        

    public void Init(PlayBoard pb, Character character1, Character character2)
    {
        _board = pb;
        _character1 = character1;
        _character2 = character2;
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
        currentPlayer.ApplyOnTimeEffects();
        currentPlayer.RemoveMarkedOnTimeEffects();

        List<List<Hexagon>> hexagons = Board.GetGrid();
        for (int i = 0; i < hexagons.Count; ++i)
        {
            foreach (Hexagon hex in hexagons[i])
            {
                hex.ApplyOnTimeEffects();
                hex.RemoveMarkedOnTimeEffects();
            }
        }

        CurrentState = State.MoveMode;
    }

    public void EndTurn()
    {
        Character currentPlayer = GetCurrentPlayer();
        //Character otherPlayer = GetOtherPlayer();
        currentPlayer.ApplyOnTimeEffects();
        currentPlayer.RemoveMarkedOnTimeEffects();

        currentPlayer.TurnNumber++;
        currentPlayer.CurrentActionPoints = Math.Min(Character._maxActionPoints, 1 + currentPlayer.TurnNumber / 5);

        _turnNumber++;
        Board._reset = true;
        BeginTurn();
    }
}

