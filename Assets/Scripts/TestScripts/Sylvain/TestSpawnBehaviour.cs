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
    }

    // Use this for initialization
    void Start () {
	    
	}

    public void changeState()
    {
        switch (_state)
        {
            case State.player1Picking:
                _state = State.player2Picking;
                break;
            case State.player2Picking:
                _state = State.spawnDone;
                break;
            default:
                break;
        }
    }

    void UpdateSpawnPositionCharacter(ref Character character)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Hexagon")))
            {
                Hexagon spawn = hitInfo.collider.gameObject.GetComponent<HexagonBehaviour>()._hexagon;
                if (spawn.IsSpawn && spawn.isReachable())
                {
                    if (character == null)
                    {
                        character = new Character(4000, spawn, _player1GameObject);
                    }
                    else
                    {
                        character.Position = spawn;
                        spawn._entity = character;
                        character._gameObject.transform.position = spawn.GameObject.transform.position + new Vector3(0, 0, 0);
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (_state)
        {
            case State.player1Picking:
                UpdateSpawnPositionCharacter(ref _player1);
                break;
            case State.player2Picking:
                UpdateSpawnPositionCharacter(ref _player2);
                break;
            case State.spawnDone:
                PlayBoardManager.GetInstance().Init(_playBoard, _player1, _player2);
                break;
            default:
                break;
        }
	}
}
