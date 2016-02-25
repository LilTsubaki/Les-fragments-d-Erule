using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestSpawnNetwork : MonoBehaviour {
    
    public string _boardName;
    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    private GameObject _board;

    public CharacterHostUI _uiPlayer1;
    public CharacterHostUI _uiPlayer2;

    public GameObject _button;

    PlayBoard _playBoard;

    Character _player1;
    Character _player2;

    private bool _gameStarted;

    void Start()
    {
        _playBoard = JSONObject.JSONToBoard(ref _board, _boardName);
        _board.AddComponent<PlayBoardBehaviour>();
        _player1 = new Character(4000, _player1GameObject);
        _player2 = new Character(4000, _player2GameObject);
        _player1.Name = "Player 1";
        _player2.Name = "Player 2";
        _uiPlayer1.Character = _player1;
        _uiPlayer2.Character = _player2;

        _gameStarted = false;

        SpellManager.getInstance();
        RunicBoardManager.GetInstance().RegisterBoard(new RunicBoard());
        PlayBoardManager.GetInstance().Init(_playBoard, _player1, _player2);

        Invoke("EndLoading", 1);
    }

    private void EndLoading()
    {
        UIManager.GetInstance().ShowPanel("PanelPosition");
        UIManager.GetInstance().FadeOutPanelNoStack("Loading");
    }

    public void changeState()
    {
        Server.State state = ServerManager.GetInstance()._server.CurrentState;
        switch (state)
        {
            case Server.State.firstPlayerPicking:
                if (PlayBoardManager.GetInstance().GetCurrentPlayer().Position != null)
                    ServerManager.GetInstance()._server.CurrentState = Server.State.secondPlayerPicking;
                else
                    Logger.Debug(PlayBoardManager.GetInstance().GetCurrentPlayer().Name + " didn't choose a position");
                break;
            case Server.State.secondPlayerPicking:
                if (PlayBoardManager.GetInstance().GetOtherPlayer().Position != null)
                {
                    ServerManager.GetInstance()._server.CurrentState = Server.State.playing;
                    UIManager.GetInstance().HideAll();
                    UIManager.GetInstance().ShowPanel("PanelPlayerBars");
                }
                else
                    Logger.Debug(PlayBoardManager.GetInstance().GetOtherPlayer().Name + " didn't choose a position");
                break;
            default:
                break;
        }
    }

    void UpdateSpawnPositionCharacter(Character character)
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
                    if (!character._gameObject.activeSelf)
                    {
                        character._gameObject.SetActive(true);
                    }
                    character.Position = spawn;
                    spawn._entity = character;
                    character._gameObject.transform.position = spawn.GameObject.transform.position + new Vector3(0, 0, 0);
                }
            }
        }
    }

    void UpdatePlaying()
    {
        if (!_gameStarted)
        {
            _button.SetActive(false);
            _playBoard.ResetBoard();
            PlayBoardManager.GetInstance().CurrentState = PlayBoardManager.State.MoveMode;
            _gameStarted = true;
        }

        PlayBoardManager.GetInstance().Board.ColorAccessibleHexagons(PlayBoardManager.GetInstance().GetCurrentPlayer());
        tryingToDoSpell();
    }

    public void EndOfTurn()
    {
        PlayBoardManager.GetInstance().EndTurn();
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
        if (PlayBoardManager.GetInstance().GetCurrentPlayer()._state != Character.State.Moving)
        {
            SpellManager.getInstance().InitSpell();
        }
    }

    // Update is called once per frame
    void Update () {
        Server.State state = ServerManager.GetInstance()._server.CurrentState;
        switch (state)
        {
            case Server.State.firstPlayerPicking:
                UpdateSpawnPositionCharacter(PlayBoardManager.GetInstance().GetCurrentPlayer());
                _button.GetComponentInChildren<Text>().text = "Valider";
                break;
            case Server.State.secondPlayerPicking:
                UpdateSpawnPositionCharacter(PlayBoardManager.GetInstance().GetOtherPlayer());
                _button.GetComponentInChildren<Text>().text = "Valider et commencer";
                break;
            case Server.State.playing:
                UpdatePlaying();
                break;
            default:
                break;
        }
    }
}
