using System;
using System.Collections.Generic;
using System.Collections;
public class Element{

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
		
}
