using UnityEngine;
using System.Collections;

public class ServerBehaviour : MonoBehaviour {


	public string _boardName;
	public GameObject _player1GameObject;
	public GameObject _player2GameObject;

	private GameObject board;

	public int posX1;
	public int posY1;
	public int posX2;
	public int posY2;

	PlayBoard playBoard;

	Character player1;
	Character player2;
	// Use this for initialization
	void Awake () {
	
		playBoard = JSONObject.JSONToBoard(ref board, _boardName);
		Hexagon hexaStart1 = playBoard.GetHexagone(posX1, posY1);
		Hexagon hexaStart2 = playBoard.GetHexagone(posX2, posY2);

		player1 = new Character(4000, hexaStart1, _player1GameObject);
		player2 = new Character(4000, hexaStart2, _player2GameObject);
		PlayBoardManager.GetInstance().Init(playBoard, player1, player2);

		Logger.logLvl = Logger.Type.TRACE;

		SpellManager.getInstance();
		RunicBoardManager.GetInstance ().RegisterBoard (new RunicBoard ());


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndOfTurn()
	{
		TurnManager.GetInstance().EndTurn();
	}

	public void tryingToDoSpell()
	{
		if(PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
		{
			/*List<Element> elementsList = _runicBoard.GetComponent<RunicBoardBehaviour>().Board.GetSortedElementList();
			Queue<Element> elements = new Queue<Element>(elementsList);
			SpellManager.getInstance().InitSpell(elements);*/


		}
	}
}
