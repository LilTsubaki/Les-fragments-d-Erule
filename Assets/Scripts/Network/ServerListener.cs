using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


public class ServerListener
{
    public Server _server;
    public TcpClient _client;
    public Character _ch;
    public bool _isRunning;

    public ServerListener(Server server, TcpClient client)
    {
        _server = server;
        _client = client;
        _client.NoDelay = true;
        _client.Client.NoDelay = true;
        _isRunning = true;
    }


    public void ListenClient()
    {
        while (_isRunning)
        {
            try {
                NetworkStream stream = _client.GetStream();
                if (stream.DataAvailable)
                {

                    int id = NetworkUtils.ReadInt(stream);
                    Logger.Warning("id: " + id);

                    switch (id)
                    {
                        case 0:
                            NetworkUtils.WriteInt(1, stream);
                            stream.Flush();
                            break;

                        case 2:
                            ReadRunicBoard();
                            break;

                        case 4:
                            ReadMakeSpell();
                            break;

                        case 7:
                            if (_server.Register(this))
                                SendCharacter();
                            else
                                RefuseCharacter();
                            break;

                        case 12:
                            if (PlayBoardManager.GetInstance().isMyTurn(_ch))
                            {
                                PlayBoardManager.GetInstance().EndTurn();
                                ServerManager.GetInstance()._server.EndTurn();
                            }
                            break;

                        case 13:
                            if (PlayBoardManager.GetInstance().isMyTurn(_ch))
                            {
                                PlayBoardManager.GetInstance().CurrentState = PlayBoardManager.State.MoveMode;
                            }
                            break;

                        case 14:
                            if (PlayBoardManager.GetInstance().isMyTurn(_ch))
                            {
                                _ch.CurrentActionPoints++;
                                PlayBoardManager.GetInstance().Board._colorAccessible = true;
                            }
                            break;

                        default:
                            Logger.Warning("Default id");
                            break;
                    }
                }
            }
            catch
            {

                Logger.Error("Client Disconnect");
                _isRunning = false;
                break;
            }
        }
        _client.Close();
        _server.RemoveClient(this);

    }


    public void Stop()
    {
        _isRunning = false;
        
    }


    void ReadRunicBoard()
    {
        Logger.Trace("ReadRunicBoard");

        Dictionary<int, Rune> map = NetworkUtils.ReadRunicBoard(_client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer1();
        rBoard.RunesOnBoard = map;
        rBoard.LogRunesOnBoard();


        NetworkUtils.WriteInt(3, _client.GetStream());
        Queue<Element> que = rBoard.GetSortedElementQueue();
        SelfSpell spell =SpellManager.getInstance ().ElementNode.GetSelfSpell (que);
        if(spell != null)
        {
            _ch.CurrentActionPoints--;
        }
        NetworkUtils.WriteBool(spell!=null, _client.GetStream());
        NetworkUtils.WriteBool( SpellManager.getInstance ().ElementNode.IsTerminal(rBoard.GetSortedElementQueue ()), _client.GetStream());

        _client.GetStream().Flush();

        PlayBoardManager.GetInstance().Board._colorAccessible = true;
    }

    void ReadMakeSpell()
    {
        
            
        Logger.Trace("ReadMakeSpell");

        

        Dictionary<int, Rune> map = NetworkUtils.ReadRunicBoard(_client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer1();
        rBoard.RunesOnBoard = map;
        rBoard.LogRunesOnBoard();


        NetworkUtils.WriteInt(5, _client.GetStream());
        Queue<Element> que = rBoard.GetSortedElementQueue();
        SelfSpell spell = SpellManager.getInstance().ElementNode.GetSelfSpell(que);
        if(spell != null)
        {
            PlayBoardManager.GetInstance().CurrentState = PlayBoardManager.State.SpellMode;
            SpellManager.getInstance().SetSpellToInit(rBoard.GetSortedElementQueue());
        }
        NetworkUtils.WriteBool(spell != null, _client.GetStream());
        NetworkUtils.WriteBool(SpellManager.getInstance().ElementNode.IsTerminal(rBoard.GetSortedElementQueue()), _client.GetStream());

        _client.GetStream().Flush();

    }

    void SendCharacter()
    {
        try { 
            Logger.Trace("SendCharacter");
            NetworkUtils.WriteInt(6, _client.GetStream());
            NetworkUtils.WriteCharacter(_ch, _client.GetStream());
            NetworkUtils.WriteBool(PlayBoardManager.GetInstance().isMyTurn(_ch), _client.GetStream());
            NetworkUtils.WriteInt(PlayBoardManager.GetInstance().TurnNumber, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch
        {
            Logger.Error("Client Disconnect");
            _isRunning = false;
        }
    }

    public void UpdateCharacter()
    {
        try { 
        Logger.Trace("UpdateCharacter");
        NetworkUtils.WriteInt(10, _client.GetStream());
        NetworkUtils.WriteCharacter(_ch, _client.GetStream());
        

        _client.GetStream().Flush();
        }
        catch
        {
            Logger.Error("Client Disconnect");
            _isRunning = false;
        }
    }

    void RefuseCharacter()
    {
        try
        {
            Logger.Trace("RefuseCharacter");
            NetworkUtils.WriteInt(8, _client.GetStream());

            _client.GetStream().Flush();

        }
        catch
        {
            Logger.Error("Client Disconnect");
            _isRunning = false;
        }
    }

    public void EndTurn()
    {
        try { 
            Logger.Trace("EndTurn");

            NetworkUtils.WriteInt(9, _client.GetStream());
            NetworkUtils.WriteCharacter(_ch, _client.GetStream());
            NetworkUtils.WriteBool(PlayBoardManager.GetInstance().isMyTurn(_ch), _client.GetStream());
            NetworkUtils.WriteInt(PlayBoardManager.GetInstance().TurnNumber, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch
        {
            Logger.Error("Client Disconnect");
            _isRunning = false;
        }
    }

    public void ResetBoard(bool fail, bool crit, int runes)
    {
        try { 
            Logger.Trace("ResetBoard");

            NetworkUtils.WriteInt(11, _client.GetStream());
            NetworkUtils.WriteBool(fail, _client.GetStream());
            NetworkUtils.WriteBool(crit, _client.GetStream());
            NetworkUtils.WriteInt(runes, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch
        {
            Logger.Error("Client Disconnect");
            _isRunning = false;
        }
    }
}
