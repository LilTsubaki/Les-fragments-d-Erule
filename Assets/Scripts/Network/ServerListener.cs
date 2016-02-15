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
                        SendCharacter();
                        break;

                    default:
                        Logger.Warning("Default id");
                        break;
                }
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
        NetworkUtils.WriteBool(spell!=null, _client.GetStream());
        NetworkUtils.WriteBool( SpellManager.getInstance ().ElementNode.IsTerminal(rBoard.GetSortedElementQueue ()), _client.GetStream());

        _client.GetStream().Flush();

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
            SpellManager.getInstance().SetSpellToInit(rBoard.GetSortedElementQueue());
        }
        NetworkUtils.WriteBool(spell != null, _client.GetStream());
        NetworkUtils.WriteBool(SpellManager.getInstance().ElementNode.IsTerminal(rBoard.GetSortedElementQueue()), _client.GetStream());

        _client.GetStream().Flush();

    }

    void SendCharacter()
    {
        Logger.Trace("SendCharacter");
        NetworkUtils.WriteInt(6, _client.GetStream());
        NetworkUtils.WriteCharacter(_ch, _client.GetStream());

        _client.GetStream().Flush();
    }
}
