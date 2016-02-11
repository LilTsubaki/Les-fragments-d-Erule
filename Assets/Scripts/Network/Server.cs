using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;

public class Server : MonoBehaviour{

    public int broadcastPort;
    public int playPort;

    TcpListener _listener;
    List<ServerListener> _clients;

    UdpClient _udpClient;

    bool _isRunning = false;

    bool _searchingClient = false;

    public void Awake()
    {
        _clients = new List<ServerListener>();
        
        _udpClient = new UdpClient(broadcastPort);
		_udpClient.EnableBroadcast = true;

        _searchingClient = true;
		_isRunning = true;
        
		StartListening(playPort);
        
		//StartCoroutine("WaitingClient");
        Thread newThread =new Thread(WaitingClient);
        newThread.Start();


    }


	void Update()
	{
		
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

}
