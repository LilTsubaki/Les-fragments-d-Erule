using UnityEngine;
using System.Collections;
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
    }

    void Update()
    {
         if (Input.GetMouseButtonDown(1))
         {
            SearchHost();
         }
    }

    public void Connect(string host, int port)
    {
        _tcpClient.Connect(host, port);
    }

    public void InitBroadCast(int port)
    {
        _udpClient = new UdpClient(port);
        _udpClient.EnableBroadcast = true;
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
        Logger.Warning("SEND TA MERE");
    }

    IEnumerator WaitHosts()
    {
        while (_isRunning && _searchingHosts)
        {
           

            yield return null;
        }

    }

}
