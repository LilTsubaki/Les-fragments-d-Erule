using System;
using System.Collections.Generic;

public class PlayBoardManager
{
	private static PlayBoardManager Instance;
	private PlayBoard _board;

	public PlayBoard Board{
		get { return _board; }
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

	public void Init(uint width, uint height, Character character1, Character character2){

		_board = new PlayBoard (width, height);
		_character1 = character1;
		_character2 = character2;
	}

	public List<Character> GetCharacterInArea(List<Hexagon> hexagons){
		List<Character> chars = new List<Character> ();

		foreach (var hex in hexagons) {
			if (_character1.Position.Equals (hex))
				chars.Add (_character1);
			if (_character2.Position.Equals (hex))
				chars.Add (_character2);
		}
		return chars;

	}
}

