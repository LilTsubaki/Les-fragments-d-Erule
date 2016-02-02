using System;
using System.Collections.Generic;

public class PlayBoardManager
{
	private static PlayBoardManager Instance;

	public static PlayBoardManager GetInstance(){
		if (Instance == null) {
			Instance = new PlayBoardManager ();
		}

		return Instance;
	}

	private PlayBoardManager ()
	{
	}

	public List<Character> GetCharacterInArea(List<Hexagon> hexagons){

		throw new NotImplementedException ();

	}
}

