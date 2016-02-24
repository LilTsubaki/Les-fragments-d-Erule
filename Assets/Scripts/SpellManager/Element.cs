using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Element : IComparable{

	public readonly int _id;
	public readonly string _name;

	private static List<Element> Elements;

	private Element(string name){
		_id = Elements.Count;
		_name = name;
	}

	public static Element GetElement(int id){
		if (Elements == null)
			Init();
		return  Elements[id];
	}

	public static List<Element> GetElements(){
		if (Elements == null)
			Init();
		return Elements;
	}

	public static void Init(){
		Elements = new List<Element> ();
		Elements.Add(new Element ("Fire"));
		Elements.Add(new Element ("Water"));
		Elements.Add(new Element ("Air"));
		Elements.Add(new Element ("Earth"));
		Elements.Add(new Element ("Wood"));
		Elements.Add(new Element ("Metal"));
    }

    public Sprite getSprite()
    {
        switch (_id)
        {
            case 0:
                return Resources.Load<Sprite>("Sprites/Feu");
            case 1:
                return Resources.Load<Sprite>("Sprites/Eau");
            case 2:
                return Resources.Load<Sprite>("Sprites/Air");
            case 3:
                return Resources.Load<Sprite>("Sprites/Terre");
            case 4:
                return Resources.Load<Sprite>("Sprites/Bois");
            case 5:
                return Resources.Load<Sprite>("Sprites/Metal");
            default:
                break;
        }
        return null;
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
            throw new ArgumentException("Object is null");
        Element element = obj as Element;
        if (element == null)
            throw new ArgumentException("Object is not an element");

        return _id - element._id;
    }
}
