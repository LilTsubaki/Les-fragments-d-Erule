using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;

public class Server : MonoBehaviour{

    public int broadcastPort;
    public int playPort;

    TcpListener _listener;
    List<TcpClient> _clients;

    UdpClient _udpClient;

    bool _isRunning = false;

    bool _searchingClient = false;

    public void Awake()
    {
        _clients = new List<TcpClient>();
        
        _udpClient = new UdpClient(broadcastPort);
		_udpClient.EnableBroadcast = true;

        _searchingClient = true;
		_isRunning = true;
        
		StartListening(playPort);
		StartCoroutine("WaitingClient");

    }


	void Update()
	{
		
	}

	public IEnumerator WaitingClient(){
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
			yield return null;
		}
	}

    private void StartListening(int port)
    {
        try
        {
			_listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            StartCoroutine("Listen");
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
                _clients.Add(client);
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

    IEnumerator Listen()
    {
		while (_isRunning && _searchingClient)
        {
            TcpClient client=ListenConnections();
            if (client != null)
            {
                Logger.Trace("Client found");
                StartCoroutine("ListenClient", client);
            }
            yield return null;
                
        }
    }

    IEnumerator ListenClient(TcpClient client)
    {
        while (_isRunning)
        {
            NetworkStream stream = client.GetStream();
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

                    default:
                        Logger.Warning("Default id");
                        break;
                }
            }

            yield return null;
        }
       
    }

}
