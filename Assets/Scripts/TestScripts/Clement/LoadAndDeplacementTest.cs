using UnityEngine;
using System.Collections;

public class LoadAndDeplacementTest : MonoBehaviour
{
    public string _boardName;
    public GameObject _player1GameObject;
    public GameObject _player2GameObject;

    private GameObject board;

    void Awake()
    {
        PlayBoard playBoard = JSONObject.JSONToBoard(ref board, _boardName);
        Hexagon hexaStart1 = playBoard.GetHexagone(0, 0);
        Hexagon hexaStart2 = playBoard.GetHexagone(6, 6);

        Character player1 = new Character(4000, hexaStart1, _player1GameObject);
        Character player2 = new Character(14298, hexaStart2, _player2GameObject);
        PlayBoardManager.GetInstance().Init(playBoard, player1, player2);
        
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
