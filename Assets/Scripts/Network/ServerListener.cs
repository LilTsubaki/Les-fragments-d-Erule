using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


public class ServerListener
{
    public Server _server;
    public TcpClient _client;
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
                        ReadRunicBoard(_client);
                        break;

                    case 4:
                        ReadMakeSpell(_client);
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


    void ReadRunicBoard(TcpClient client)
    {
        Logger.Trace("ReadRunicBoard");

        Dictionary<int, Rune> map = NetworkUtils.ReadRunicBoard(client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer1();
        rBoard.RunesOnBoard = map;
        rBoard.LogRunesOnBoard();


        NetworkUtils.WriteInt(3, client.GetStream());
        Queue<Element> que = rBoard.GetSortedElementQueue();
        SelfSpell spell =SpellManager.getInstance ().ElementNode.GetSelfSpell (que);
        NetworkUtils.WriteBool(spell!=null, client.GetStream());
        NetworkUtils.WriteBool( SpellManager.getInstance ().ElementNode.IsTerminal(rBoard.GetSortedElementQueue ()), client.GetStream());

        client.GetStream().Flush();

    }

    void ReadMakeSpell(TcpClient client)
    {
        Logger.Trace("ReadMakeSpell");

        Dictionary<int, Rune> map = NetworkUtils.ReadRunicBoard(client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer1();
        rBoard.RunesOnBoard = map;
        rBoard.LogRunesOnBoard();


        NetworkUtils.WriteInt(5, client.GetStream());
        Queue<Element> que = rBoard.GetSortedElementQueue();
        SelfSpell spell = SpellManager.getInstance().ElementNode.GetSelfSpell(que);
        if(spell != null)
        {
            SpellManager.getInstance().SetSpellToInit(rBoard.GetSortedElementQueue());
        }
        NetworkUtils.WriteBool(spell != null, client.GetStream());
        NetworkUtils.WriteBool(SpellManager.getInstance().ElementNode.IsTerminal(rBoard.GetSortedElementQueue()), client.GetStream());

        client.GetStream().Flush();

    }
}
