using System;
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

    public int CompareTo(object obj)
    {
        if (obj == null)
            throw new ArgumentException("Object is null");
        Element element = obj as Element;
        if (element == null)
            throw new ArgumentException("Object is not an element");

        return element._id - _id;
    }

    public static Queue<Element> GetSortedQueueFromList(List<Element> elements)
    {
        elements.Sort();
        return new Queue<Element>(elements);
    }
		
}
