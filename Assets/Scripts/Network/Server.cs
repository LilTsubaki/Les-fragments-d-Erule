using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;

public class Server : MonoBehaviour{

    float timeout=30;
    public float currrentTimeout;

    public int broadcastPort;
    public int playPort;

    TcpListener _listener;
    List<ServerListener> _clients;

    ServerListener _client1;
    ServerListener _client2;

    UdpClient _udpClient;

    bool _isRunning = false;

    bool _searchingClient = false;

    public void Awake()
    {
        currrentTimeout = timeout;
        ServerManager.GetInstance().Init(this);
        _clients = new List<ServerListener>();
        
        _udpClient = new UdpClient(broadcastPort);
		_udpClient.EnableBroadcast = true;

        _searchingClient = true;
		_isRunning = true;
        
		StartListening(playPort);
        Thread newThread =new Thread(WaitingClient);
        newThread.Start();


    }

    public void OnDestroy()
    {
        _isRunning = false;

        _searchingClient = false;

        _udpClient.Close();

        foreach(var cli in _clients)
        {
            cli.Stop();
        }
    }


    void FixedUpdate()
	{
        currrentTimeout -= Time.fixedDeltaTime;
        if (currrentTimeout <= 0)
        {
            TurnManager.GetInstance().EndTurn();
            ServerManager.GetInstance()._server.EndTurn();
        }
	}

	public void WaitingClient(){
		while (_isRunning && _searchingClient) {
			if(_udpClient.Available>=4){
				
				IPEndPoint ip=new IPEndPoint(IPAddress.Any, broadcastPort);
				byte[] data =_udpClient.Receive(ref ip);

				if (BitConverter.IsLittleEndian)
					Array.Reverse(data);

				int id = BitConverter.ToInt32(data, 0);

				if (id == 0){
					Logger.Warning("id: "+id);

				


					byte[] data2;
					data2 = BitConverter.GetBytes(1);
					if (BitConverter.IsLittleEndian)
						Array.Reverse(data2);

					_udpClient.Send(data2, data.Length, ip);
				}
			}
		}
        _udpClient.Close();
      }

    public bool Register(ServerListener serverListener)
    {
        if (_client1 == null)
        {
            _client1 = serverListener;
            _client1._ch = PlayBoardManager.GetInstance().Character1;
            return true;
        }

        if (_client2 == null)
        {
            _client2 = serverListener;
            _client2._ch = PlayBoardManager.GetInstance().Character2;
            _searchingClient = false;
            CleanClient();
            return true;
        }

        return false;

    }

    public void CleanClient()
    {
        foreach (var client in _clients)
        {
            if (client != _client1 && client != _client2)
            {
                client.Stop();
                _clients.Remove(client);
            }
        }
    }

    private void StartListening(int port)
    {
        try
        {
			_listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            //StartCoroutine("Listen");
            Thread newThread = new Thread(Listen);
            newThread.Start();
        }
        catch
        { }
    }

    public TcpClient ListenConnections()
    {
        try
        {
            if (_listener.Pending())
            {
                TcpClient client = _listener.AcceptTcpClient();
                return client;
            }
        }
        catch
        { }
        return null;
    }

    public void Stop()
    {
        _listener.Stop();
        _isRunning = false;
    }

    void Listen()
    {
		while (_isRunning && _searchingClient)
        {
            TcpClient client=ListenConnections();
            if (client != null)
            {
                Logger.Trace("Client found");
                //StartCoroutine("ListenClient", client);
                ServerListener listener = new ServerListener(this, client);
                _clients.Add(listener);
                Thread newThread = new Thread(listener.ListenClient);
                newThread.Start();

                
            }
                
        }
    }

    public void RemoveClient(ServerListener client)
    {
        _clients.Remove(client);
    }

    public void EndTurn()
    {
        if(_client1!=null)
            _client1.EndTurn();
        if (_client2 != null)
            _client2.EndTurn();

        currrentTimeout = timeout;
    }

    public void UpdateCharacter(Character character)
    {
        if(_client1 != null && _client1._ch == character)
        {
            _client1.UpdateCharacter();
            return;
        }

        if (_client2 !=null && _client2._ch == character )
        {
            _client2.UpdateCharacter();
            return;
        }
    }

    public void ApplyEffects(Character character, bool fail, bool crit, int runes)
    {


        if (_client1 != null)
        {
            _client1.UpdateCharacter();
            if (_client1._ch == character)
            {
                _client1.ResetBoard(fail, crit, runes);
            }
        }

        if (_client2 != null)
        {
            _client2.UpdateCharacter();
            if (_client2._ch == character)
            {
                _client2.ResetBoard(fail, crit, runes);
            }
        }
    }
}
