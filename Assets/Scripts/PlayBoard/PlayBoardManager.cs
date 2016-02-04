using System;
using System.Collections.Generic;

public class PlayBoardManager
{
	private static PlayBoardManager Instance;
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

	public static PlayBoardManager GetInstance(){
		if (Instance == null) {
			Instance = new PlayBoardManager ();
		}

		return Instance;
	}

	private PlayBoardManager ()
	{
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
}

