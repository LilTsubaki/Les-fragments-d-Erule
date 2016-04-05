using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


public class ServerListener
{
    public Server _server;
    public TcpClient _client;
    public Character _character;
    public bool _isRunning;

    public string _name;

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
                            
                        case 12:
                            if (PlayBoardManager.GetInstance().isMyTurn(_character))
                            {
                                PlayBoardManager.GetInstance().CanEndTurn = true;
                                //PlayBoardManager.GetInstance().EndTurn();

                                
                            }
                            break;

                        case 13:
                            if (PlayBoardManager.GetInstance().isMyTurn(_character))
                            {
                                PlayBoardManager.GetInstance().CurrentState = PlayBoardManager.State.MoveMode;
                            }
                            break;

                        case 14:
                            if (PlayBoardManager.GetInstance().isMyTurn(_character))
                            {
                                _character.CurrentActionPoints++;
                                PlayBoardManager.GetInstance().Board._colorAccessible = true;
                            }
                            break;

                        case 15:

                            String name = NetworkUtils.ReadString(_client.GetStream());
                            NetworkUtils.WriteInt(16, _client.GetStream());
                            if (_server.Register(this))
                            {
                                Logger.Debug("registered");
                                _name = name;
                                NetworkUtils.WriteBool(true, _client.GetStream());
                            }
                            else
                            {
                                NetworkUtils.WriteBool(false, _client.GetStream());
                            }
                            _client.GetStream().Flush();
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
        bool removeActionPoint = NetworkUtils.ReadBool(_client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer();
        rBoard.RunesOnBoard = map;
        rBoard.LogRunesOnBoard();

        NetworkUtils.WriteInt(3, _client.GetStream());
        Queue<Element> que = rBoard.GetSortedElementQueue();
        TargetSpell spell =SpellManager.getInstance ().ElementNode.GetTargetSpell( que);

        //Logger.Error("spell != null" + (spell != null));
        //Logger.Error("WTF");
        Logger.Debug("Is terminal : " + SpellManager.getInstance().ElementNode.IsTerminal(rBoard.GetSortedElementQueue()));
        NetworkUtils.WriteBool(spell!=null, _client.GetStream());
        NetworkUtils.WriteBool( SpellManager.getInstance ().ElementNode.IsTerminal(rBoard.GetSortedElementQueue ()), _client.GetStream());

        /* private Orientation.EnumOrientation _orientation;*/
        if(spell != null)
        {
            if (removeActionPoint)
            {
                _character.CurrentActionPoints--;
            }
            PlayBoardManager.GetInstance().Board._colorAccessible = true;

            Range range = SpellManager.getInstance().GetRangeById(spell._rangeId);
            Area area = SpellManager.getInstance().GetAreaById(spell.AreaId);
            //write min range and max range
            NetworkUtils.WriteInt(range.MinRange, _client.GetStream());
            NetworkUtils.WriteInt(range.MaxRange, _client.GetStream());

            //write isPiercing and isEnemyTargetable
            NetworkUtils.WriteBool(range.Piercing, _client.GetStream());
            NetworkUtils.WriteBool(range.EnemyTargetable, _client.GetStream());

            //write orientation
            NetworkUtils.WriteOrientation(range.Orientation, _client.GetStream());

            //write area
            NetworkUtils.WriteArea(area, _client.GetStream());

            PlayBoardManager.GetInstance().GetCurrentPlayer().NextState = Character.State.CastingSpell;
            HashSet<Element> elemSet = new HashSet<Element>(rBoard.GetSortedElementList());

            PlayBoardManager.GetInstance().GetCurrentPlayer().SetOrbs(elemSet.ToList());
        }
        else
        {
            PlayBoardManager.GetInstance().GetCurrentPlayer().NextState = Character.State.Waiting;
        }

        if(rBoard.GetSortedElementList().Count == 0)
        {
            PlayBoardManager.GetInstance().GetCurrentPlayer().SetOrbs(new List<Element>());
        }
        

        _client.GetStream().Flush();

        
    }

    void ReadMakeSpell()
    {
        
            
        Logger.Trace("ReadMakeSpell");

        

        Dictionary<int, Rune> map = NetworkUtils.ReadRunicBoard(_client.GetStream());
        RunicBoard rBoard = RunicBoardManager.GetInstance().GetBoardPlayer();
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

    public void SendCharacter()
    {
        try { 
            Logger.Trace("SendCharacter");
            NetworkUtils.WriteInt(6, _client.GetStream());
            NetworkUtils.WriteCharacter(_character, _client.GetStream());
            NetworkUtils.WriteBool(PlayBoardManager.GetInstance().isMyTurn(_character), _client.GetStream());
            NetworkUtils.WriteInt(PlayBoardManager.GetInstance().TurnNumber, _client.GetStream());
            NetworkUtils.WriteFloat(ServerManager.GetInstance()._server.currrentTimeout, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch (Exception e)
        {
            Logger.Error("send character failed : " + e.StackTrace);
            _isRunning = false;
        }
    }

    public void UpdateCharacter()
    {
        try { 
        Logger.Trace("UpdateCharacter");
        NetworkUtils.WriteInt(10, _client.GetStream());
        NetworkUtils.WriteCharacter(_character, _client.GetStream());
        

        _client.GetStream().Flush();
        }
        catch (Exception e)
        {
            Logger.Error("update character failed : " + e.StackTrace);
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
        catch (Exception e)
        {
            Logger.Error("end turn failed : " + e.StackTrace);
            _isRunning = false;
        }
    }

    public void EndTurn()
    {
        try { 
            Logger.Trace("EndTurn");

            NetworkUtils.WriteInt(9, _client.GetStream());
            NetworkUtils.WriteCharacter(_character, _client.GetStream());
            NetworkUtils.WriteBool(PlayBoardManager.GetInstance().isMyTurn(_character), _client.GetStream());
            NetworkUtils.WriteInt(PlayBoardManager.GetInstance().TurnNumber, _client.GetStream());
            NetworkUtils.WriteFloat(ServerManager.GetInstance()._server.currrentTimeout, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch (Exception e)
        {
            Logger.Error("end turn failed : " + e.StackTrace);
            _isRunning = false;
        }
    }

    public void ResetBoard(bool success, bool crit, int runes)
    {
        try { 
            Logger.Trace("ResetBoard");

            NetworkUtils.WriteInt(11, _client.GetStream());
            NetworkUtils.WriteBool(success, _client.GetStream());
            NetworkUtils.WriteBool(crit, _client.GetStream());
            NetworkUtils.WriteInt(runes, _client.GetStream());

            _client.GetStream().Flush();
        }
        catch(Exception e)
        {
            Logger.Error("reset board failed : " + e.StackTrace);
            _isRunning = false;
        }
    }
}
