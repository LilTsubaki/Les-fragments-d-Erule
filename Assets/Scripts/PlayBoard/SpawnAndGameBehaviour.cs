using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnAndGameBehaviour : MonoBehaviour {
    
    public string _boardName;
    public string _prefabEnvironmentName;

    public GameObject _player1GameObject;
    public GameObject _player2GameObject;
    private GameObject _board;

    public CharacterHostUI _uiPlayer1;
    public CharacterHostUI _uiPlayer2;

    public GameObject _button;
    public Text _textPlayerToPlace;

    PlayBoard _playBoard;

    Character _player1;
    Character _player2;

    private bool _gameStarted;

    private GameObject _environment;

    void Start()
    {
        CameraManager.GetInstance().FadeTo("cameraBoard", 1);
        
        

        _playBoard = JSONObject.JSONToBoard(ref _board, _boardName);
        if (_prefabEnvironmentName != "")
        {
            GameObject prefab = Resources.Load<GameObject>("prefabs/maps/" + _prefabEnvironmentName);
            _environment = Instantiate(prefab);
            _board.transform.GetChild(0).gameObject.SetActive(false);
        }

        _board.AddComponent<PlayBoardBehaviour>();
        _player1 = new Character(3000, _player1GameObject);
		_player2 = new Character(3000, _player2GameObject);
        /*_player1.Name = ServerManager.GetInstance()._server.Client1._name;
        _player2.Name = ServerManager.GetInstance()._server.Client2._name;*/
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
        UIManager.GetInstance().ShowPanel("PanelAroundMap");
        UIManager.GetInstance().FadeOutPanelNoStack("Loading");
    }

    public void changeState()
    {
        Server.State state = ServerManager.GetInstance()._server.CurrentState;
        switch (state)
        {
            case Server.State.turningAroundMap:
                ServerManager.GetInstance()._server.CurrentState = Server.State.firstPlayerPicking;
                break;
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
                    ServerManager.GetInstance()._server.SendCharacter();
                    UIManager.GetInstance().HideAll();
                    UIManager.GetInstance().ShowPanel("menuButton");
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
            Ray camRay = CameraManager.GetInstance().Active.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Hexagon")))
            {
                Hexagon spawn = hitInfo.collider.gameObject.GetComponent<HexagonBehaviour>()._hexagon;
                if (spawn.IsSpawn && spawn.isReachable())
                {
                    
                    for(int i = 0; i < 2; ++i)
                    {
                        character.GameObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (!character.GameObject.activeSelf)
                    {
                        character.GameObject.SetActive(true);
                    }
                    Animator anim = character.GameObject.GetComponent<Animator>();
                    anim.SetTrigger("EnterFight");
                    character.Position = spawn;
                    spawn._entity = character;
                    character.GameObject.transform.position = spawn.GameObject.transform.position + new Vector3(0, 0.0f, 0);

                    /*for (int i = 0; i < 2; ++i)
                    {
                        character.GameObject.transform.GetChild(i).gameObject.SetActive(true);
                    }*/
                    /*if (!anim.GetBool("BeginFight")) {
                        anim.SetBool("BeginFight", true);
                    }*/
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
        if (PlayBoardManager.GetInstance().GetCurrentPlayer().CurrentState != Character.State.Moving)
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
                _textPlayerToPlace.text = "C'est au tour de\n" + PlayBoardManager.GetInstance().GetCurrentPlayer().Name + "\n de se placer.\n";
                break;
            case Server.State.secondPlayerPicking:
                UpdateSpawnPositionCharacter(PlayBoardManager.GetInstance().GetOtherPlayer());
                _button.GetComponentInChildren<Text>().text = "Valider et commencer";
                _textPlayerToPlace.text = "C'est au tour de\n" + PlayBoardManager.GetInstance().GetOtherPlayer().Name + "\n de se placer.\n";
                break;
            case Server.State.playing:
                UpdatePlaying();
                break;
            default:
                break;
        }
    }
}
