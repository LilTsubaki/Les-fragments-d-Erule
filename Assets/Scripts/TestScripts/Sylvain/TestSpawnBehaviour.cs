using UnityEngine;
using System.Collections;

public class TestSpawnBehaviour : MonoBehaviour {

    enum State { player1Picking, player2Picking, spawnDone }

    public string _boardName;
    public GameObject _player1GameObject;
    public GameObject _player2GameObject;

    private GameObject _board;
    
    PlayBoard _playBoard;

    Character _player1;
    Character _player2;

    private State _state;

    // Use this for initialization
    void Awake()
    {

        _playBoard = JSONObject.JSONToBoard(ref _board, _boardName);
        //player1 = new Character(4000, hexaStart1, _player1GameObject);
        //player2 = new Character(4000, hexaStart2, _player2GameObject);
        //PlayBoardManager.GetInstance().Init(playBoard, player1, player2);
    }

    // Use this for initialization
    void Start () {
	    
	}

    void UpdatePosition(ref Character character)
    {

    }
	
	// Update is called once per frame
	void Update () {
	
        switch (_state)
        {
            case State.player1Picking:
                UpdatePosition(ref _player1);
                break;
            case State.player2Picking:
                UpdatePosition(ref _player2);
                break;
            case State.spawnDone:
                break;
            default:
                break;
        }

	}
}
