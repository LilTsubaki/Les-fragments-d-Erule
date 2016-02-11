using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System;
using System.Collections.Generic;

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

	public static void WriteBool(bool b, NetworkStream stream)
	{
		byte[] data;
		data = BitConverter.GetBytes(b);

		stream.Write(data,0, sizeof(bool));
	}

	public static bool ReadBool(NetworkStream stream)
	{
		byte[] data = new byte[sizeof(bool)];

		stream.Read(data, 0, sizeof(bool));
		return BitConverter.ToBoolean(data, 0);
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

    public static void WriteElement(Element element, NetworkStream stream)
    {
       WriteInt(element._id, stream);
    }

    public static Element ReadElement(NetworkStream stream)
    {
        return Element.GetElement(ReadInt(stream));
    }
     
    public static void WriteRunicBoard(RunicBoard runicBoard, NetworkStream stream)
    {
        WriteInt(runicBoard.RunesOnBoard.Count, stream);
        foreach (KeyValuePair<int, Rune> entry in runicBoard.RunesOnBoard)
        {
            WriteInt(entry.Key, stream);
            WriteElement(entry.Value.Element, stream);
        }
    }

    public static Dictionary<int, Rune> ReadRunicBoard(NetworkStream stream)
    {
        Dictionary<int, Rune> map = new Dictionary<int, Rune>();
        int nbRune = ReadInt(stream);
        for(int i = 0; i<nbRune; ++i)
        {
            int position = ReadInt(stream);
            Rune rune = new Rune(ReadElement(stream), position);
            map.Add(position, rune);
        }

        return map;
    }

}
