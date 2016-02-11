using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Net;

public class Client : MonoBehaviour{

    public int broadcastPort;
    public int playPort;
    TcpClient _tcpClient;
    UdpClient _udpClient;
    bool _isRunning;
    bool _searchingHosts;

    public void Awake()
    {
       
        _tcpClient = new TcpClient();
        InitBroadCast(broadcastPort);
        _searchingHosts = true;
        StartCoroutine("WaitHosts");
        ClientManager.GetInstance().Init(this);
    }

    void Update()
    {
         if (Input.GetMouseButtonDown(1))
         {
            SearchHost();
            //Connect("159.84.141.84", playPort);
         }
    }

    public void Connect(string host, int port)
    {
        Logger.Trace("Try Connect to " + host + ":" + port);
        _tcpClient.Connect(host, port);
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

    IEnumerator WaitHosts()
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
                        break;
                    default:
                        Logger.Error("Nope.");
                        break;
                }
            }

            yield return null;
        }

    }
    
    public bool ReadMessage(int id)
    {
        switch (id)
        {
            default:
                return false;
        }

    }
    public SendBoardResponse SendBoard()
    {
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
            return new SendBoardResponse(NetworkUtils.ReadBool(_tcpClient.GetStream()), NetworkUtils.ReadBool(_tcpClient.GetStream()));
        }

        else
        {
            return null;
        }

    }

}
