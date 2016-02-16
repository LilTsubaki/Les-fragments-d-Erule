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
        tryingToDoSpell();
        resetBoard();
    }

	public void EndOfTurn()
	{
		TurnManager.GetInstance().EndTurn();
        ServerManager.GetInstance()._server.EndTurn();
	}

    public void resetBoard()
    {
        if (PlayBoardManager.GetInstance().Board._reset)
        {
            PlayBoardManager.GetInstance().Board._reset = false;
            PlayBoardManager.GetInstance().Board.ResetBoard();
        }
    }

    public void tryingToDoSpell()
	{
		if(PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
		{
            SpellManager.getInstance().InitSpell();
		}
	}
}
