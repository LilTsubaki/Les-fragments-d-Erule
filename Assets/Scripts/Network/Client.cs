using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Net;
using System.Threading;

public class Client : MonoBehaviour{

    public int broadcastPort;
    public int playPort;
    TcpClient _tcpClient;
    UdpClient _udpClient;
    bool _isRunning;
    bool _searchingHosts;
    bool _isMyTurn;
    bool _isListening;

    bool _isListeningThreadReading;
    bool _isMainThreadReading;

    bool _resetBoard;
    int _runeKept;

    public CharacterUI _characterUI;

    Character _currentCharacter;

    public Character CurrentCharacter
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

    public void Awake()
    {
       
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
        
    }

    void Update()
    {
        if (_resetBoard)
        {
            RunicBoardManager.GetInstance()._runicBoardBehaviour.ResetRunes(_runeKept);
            /*
            switch (_runeKept)
            {

                
                case 0:
                    RunicBoardManager.GetInstance()._runicBoardBehaviour.ResetRunes();
                    break;
                case 1:
                    RunicBoardManager.GetInstance().GetBoardPlayer1().RemoveAllRunesExceptHistory(true);
                    break;
                case 2:
                    RunicBoardManager.GetInstance().GetBoardPlayer1().RemoveAllRunesExceptHistory(false);
                    break;
            }
            */
            _resetBoard = false;
        }
    }

    public void Connect(string host, int port)
    {
        Logger.Trace("Try Connect to " + host + ":" + port);
        _tcpClient.Connect(host, port);
        Thread newThread = new Thread(WaitingMessage);
        newThread.Start();
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
                _currentCharacter = NetworkUtils.ReadCharacter(_tcpClient.GetStream());
                _isMyTurn = NetworkUtils.ReadBool(_tcpClient.GetStream());
                Logger.Debug("nb action points : " + _currentCharacter.CurrentActionPoints);
                return true;

            //updating char infos
            case 10:
                _currentCharacter = NetworkUtils.ReadCharacter(_tcpClient.GetStream());
                Logger.Debug("current action points : " + _currentCharacter.CurrentActionPoints);
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
                        Connect(ip.Address.ToString(), playPort);
                        _searchingHosts = false;
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
            _currentCharacter =  NetworkUtils.ReadCharacter(_tcpClient.GetStream());
            _characterUI.SetCharacter(_currentCharacter);
            _isMyTurn = NetworkUtils.ReadBool(_tcpClient.GetStream());
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
}
