using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System;

public class NetworkUtils {


    public static void WriteInt(int i, NetworkStream stream)
    {
        byte[] data;
        data = BitConverter.GetBytes(i);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(data);

        stream.Write(data,0, sizeof(int));
    }

    public static int ReadInt(NetworkStream stream)
    {
        byte[] data = new byte[sizeof(int)];
       

        stream.Read(data, 0, sizeof(int));
        
        if (BitConverter.IsLittleEndian)
            Array.Reverse(data);

        return BitConverter.ToInt32(data, 0);
    }


    public static void WriteChar(char c, NetworkStream stream)
    {
        byte[] data;
        data = BitConverter.GetBytes(c);

        stream.Write(data, 0, sizeof(char));
    }

    public static char ReadChar(NetworkStream stream)
    {
        byte[] data = new byte[sizeof(char)];

        stream.Read(data, 0, sizeof(char));

        return BitConverter.ToChar(data, 0);
    }

    public static void WriteString(string str, NetworkStream stream)
    {

        WriteInt(str.Length, stream);

        foreach(var c in str)
        {
            WriteChar(c, stream);
        }
    }

    public static string ReadString(NetworkStream stream)
    {
        int length = ReadInt(stream);
        StringWriter sw=new StringWriter();
        for(var i=0; i<length; ++i)
        {
            sw.Write(ReadChar(stream));
        }

        return sw.ToString();

    }

}
