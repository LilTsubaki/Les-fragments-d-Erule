using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Net;
using System.Threading;

public class Client : MonoBehaviour{

    public int broadcastPort;
    public int playPort;
    public int _turnNumber = 0;
    TcpClient _tcpClient;
    UdpClient _udpClient;
    bool _isRunning;
    bool _searchingHosts;
    bool _isMyTurn;
    bool _isListening;

    bool _isListeningThreadReading;
    bool _isMainThreadReading;

    bool _resetBoard;
    bool _lockedMode;
    int _runeKept;

    public GameObject _buttonHost;
    public GameObject _scrollPanel;
    Dictionary<string, int> _hostsReceived;
    bool _newHost;

    Character _currentCharacter;

    public Character CurrentCharacter
    {
        get
        {
            return CurrentCharacter1;
        }

        set
        {
            CurrentCharacter1 = value;
        }
    }

    public bool IsMyTurn
    {
        get
        {
            return _isMyTurn;
        }

        set
        {
            _isMyTurn = value;
        }
    }

    public bool LockedMode
    {
        get
        {
            return _lockedMode;
        }

        set
        {
            _lockedMode = value;
        }
    }

    public Character CurrentCharacter1
    {
        get
        {
            return _currentCharacter;
        }

        set
        {
            _currentCharacter = value;
        }
    }

    public void Awake()
    {
        _lockedMode = false;
        _tcpClient = new TcpClient();
        _tcpClient.NoDelay = true;
        _tcpClient.Client.NoDelay = true;
        IsMyTurn = false;
        _isListening = true;
        _isListeningThreadReading = false;
        _isMainThreadReading = false;
        _resetBoard = false;
        InitBroadCast(broadcastPort);
        _searchingHosts = true;
        Thread newThread = new Thread(WaitHosts);
        newThread.Start();
        _runeKept = 0;
        ClientManager.GetInstance().Init(this);
        _hostsReceived = new Dictionary<string, int>();
    }

    void Update()
    {
        if (_resetBoard)
        {
            RunicBoardManager.GetInstance()._runicBoardBehaviour.ResetRunes(_runeKept);
            if(ClientManager.GetInstance()._client.LockedMode)
                UIManager.GetInstance().ToggleButton.GetComponent<ToggleBehaviour>().SwitchMode();

            _resetBoard = false;
        }

        if (_newHost)
        {
            foreach(KeyValuePair<string, int> host in _hostsReceived)
            {
                AddHostToScene(host.Key, host.Value);
            }

            _hostsReceived.Clear();
            _newHost = false;
        }
    }

    void OnDestroy()
    {
        _isRunning = false;
        _searchingHosts = false;
        _udpClient.Close();
        Disconnect();
    }

    private void AddHostToScene(string host, int port)
    {
        GameObject connectButton = Instantiate(_buttonHost);
        connectButton.SetActive(true);
        Button button = connectButton.GetComponent<Button>();
        connectButton.transform.SetParent(_scrollPanel.transform);
        connectButton.transform.localPosition = new Vector3(80, (_scrollPanel.transform.childCount) * -30);
        button.GetComponentInChildren<Text>().text = host + ":" + port;
        button.onClick.AddListener(delegate { Connect(host, port); RequestCharacter(); });
    }

    private void addHostToList(string host, int port)
    {
        _hostsReceived.Add(host, port);
        _newHost = true;
    }

    public void Connect(string host, int port)
    {
        UIManager.GetInstance().HideAll();
        UIManager.GetInstance().ShowPanel("PanelGame");
        Logger.Trace("Try Connect to " + host + ":" + port);
        _tcpClient.Connect(host, port);
        Thread newThread = new Thread(WaitingMessage);
        newThread.Start();
        _searchingHosts = false;
    }

    public void WaitingMessage()
    {
        while (_isListening)
        {
            NetworkStream stream = _tcpClient.GetStream();
            if (stream.DataAvailable && _isMainThreadReading == false)
            {

                _isListeningThreadReading = true;
                ReadMessage(NetworkUtils.ReadInt(stream));
                _isListeningThreadReading = false;
            }
        }
    }

    public bool ReadMessage(int id)
    {
        switch (id)
        {
            //end of turn
            case 9:
                CurrentCharacter1 = NetworkUtils.ReadCharacter(_tcpClient.GetStream());
                _isMyTurn = NetworkUtils.ReadBool(_tcpClient.GetStream());
                _turnNumber = NetworkUtils.ReadInt(_tcpClient.GetStream());
                Logger.Debug("nb action points : " + CurrentCharacter1.CurrentActionPoints);
                _lockedMode = false;
                return true;

            //updating char infos
            case 10:
                CurrentCharacter1 = NetworkUtils.ReadCharacter(_tcpClient.GetStream());
                Logger.Debug("current action points : " + CurrentCharacter1.CurrentActionPoints);
                return true;

            case 11:
                //TODO
                Logger.Debug("receive reset board request");
                bool fail = NetworkUtils.ReadBool(_tcpClient.GetStream());
                bool crit = NetworkUtils.ReadBool(_tcpClient.GetStream());
                _runeKept = NetworkUtils.ReadInt(_tcpClient.GetStream());
                _resetBoard = true;
                return true;

            default:
                return false;
        }

    }

    public void Disconnect()
    {
        _tcpClient.Close();
        Logger.Warning("disconnected");
    }

    public void InitBroadCast(int port)
    {
        _udpClient = new UdpClient(port);
        _udpClient.EnableBroadcast = true;
        _isRunning = true;
    }

    public void SearchHost()
    {
        byte[] data;
        data = BitConverter.GetBytes(0);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(data);

        IPAddress broadcast = IPAddress.Broadcast;
        IPEndPoint ep = new IPEndPoint(broadcast, broadcastPort);

        _udpClient.Send(data, data.Length, ep);

        UIManager.GetInstance().ShowPanel("PanelServers");

        foreach(Transform child in _scrollPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void WaitHosts()
    {
        while (_isRunning && _searchingHosts)
        {
            //Logger.Warning(" blop " + _udpClient.Available);
           if(_udpClient.Available >= 4)
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, broadcastPort);
                byte[] data = _udpClient.Receive(ref ip);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(data);

                int id = BitConverter.ToInt32(data, 0);
                Logger.Warning("id host : " + id + " / " + ip.Address.ToString());
                switch (id)
                {
                    case 1:
                        Logger.Warning("Connect to " + ip.Address.ToString());
                        //Connect(ip.Address.ToString(), playPort);
                        addHostToList(ip.Address.ToString(), playPort);
                        break;
                    default:
                        Logger.Error("Nope.");
                        break;
                }
            }
        }

    }


    public void SendMakeSpell()
    {
        while (_isListeningThreadReading) ;

        _isMainThreadReading = true;
        NetworkUtils.WriteInt(4, _tcpClient.GetStream());
        NetworkUtils.WriteRunicBoard(RunicBoardManager.GetInstance().GetBoardPlayer1(), _tcpClient.GetStream());
        _tcpClient.GetStream().Flush();


        int id;
        do
        {
            id = NetworkUtils.ReadInt(_tcpClient.GetStream());
        }
        while (ReadMessage(id));


        if (id == 5)
        {
            SendBoardResponse sbr = new SendBoardResponse(NetworkUtils.ReadBool(_tcpClient.GetStream()), NetworkUtils.ReadBool(_tcpClient.GetStream()));
        }
        _isMainThreadReading = false;
    }


    public SendBoardResponse SendBoard()
    {

        while (_isListeningThreadReading) ;

        _isMainThreadReading = true;
        Logger.Debug("send board");
        NetworkUtils.WriteInt(2, _tcpClient.GetStream());
        
        NetworkUtils.WriteRunicBoard(RunicBoardManager.GetInstance().GetBoardPlayer1(), _tcpClient.GetStream());
        _tcpClient.GetStream().Flush();

        int id;
        do
        {
            id = NetworkUtils.ReadInt(_tcpClient.GetStream());
        }
        while (ReadMessage(id)) ;
        

        if (id == 3)
        {
            SendBoardResponse sbr = new SendBoardResponse(NetworkUtils.ReadBool(_tcpClient.GetStream()), NetworkUtils.ReadBool(_tcpClient.GetStream()));
            _isMainThreadReading = false;
            return sbr;
        }

        else
        {
            _isMainThreadReading = false;
            return null;
        }
        
    }

    public void RequestCharacter()
    {
        while (_isListeningThreadReading);

        _isMainThreadReading = true;
        Logger.Debug("send request character");
        NetworkUtils.WriteInt(7, _tcpClient.GetStream());
        _tcpClient.GetStream().Flush();

        int id;
        do
        {
            id = NetworkUtils.ReadInt(_tcpClient.GetStream());
        }
        while (ReadMessage(id));


        if (id == 6)
        {
            Logger.Debug("read character");
            CurrentCharacter1 =  NetworkUtils.ReadCharacter(_tcpClient.GetStream());
            _isMyTurn = NetworkUtils.ReadBool(_tcpClient.GetStream());
            _turnNumber = NetworkUtils.ReadInt(_tcpClient.GetStream());
        }

        else
        {
            if(id == 8)
            {
                Logger.Error("Connection refused");
            }
        }
        _isMainThreadReading = false;
    }

    public void SendEndTurn()
    {
        while (_isListeningThreadReading) ;

        _isMainThreadReading = true;
        NetworkUtils.WriteInt(12, _tcpClient.GetStream());
        _tcpClient.GetStream().Flush();
        _isMainThreadReading = false;
    }

    public void SendMovementMode()
    {
        if(_isMyTurn)
        {
            while (_isListeningThreadReading) ;

            _isMainThreadReading = true;
            NetworkUtils.WriteInt(13, _tcpClient.GetStream());
            _tcpClient.GetStream().Flush();
            _isMainThreadReading = false;
        }

    }

    public void SendRemoveRune()
    {
        if (_isMyTurn)
        {
            while (_isListeningThreadReading) ;

            _isMainThreadReading = true;
            NetworkUtils.WriteInt(14, _tcpClient.GetStream());
            _tcpClient.GetStream().Flush();
            _isMainThreadReading = false;
        }

    }
}
