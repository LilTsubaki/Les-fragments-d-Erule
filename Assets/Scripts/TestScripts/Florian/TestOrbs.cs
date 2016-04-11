using UnityEngine;
using System.Collections.Generic;

public class TestOrbs : MonoBehaviour {

    public Orbs _orbs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            List<Element> elems = new List<Element>();
            elems.Add(Element.GetElement(0));
            elems.Add(Element.GetElement(2));
            
            _orbs.SetElements(elems);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            List<Element> elems = new List<Element>();
            elems.Add(Element.GetElement(4));
            elems.Add(Element.GetElement(1));
            elems.Add(Element.GetElement(3));

            _orbs.SetElements(elems);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            List<Element> elems = new List<Element>();
            elems.Add(Element.GetElement(0));
            elems.Add(Element.GetElement(1));
            elems.Add(Element.GetElement(2));
            elems.Add(Element.GetElement(3));
            elems.Add(Element.GetElement(4));
            elems.Add(Element.GetElement(5));

            _orbs.SetElements(elems);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            List<Element> elems = new List<Element>();
            elems.Add(Element.GetElement(4));
            elems.Add(Element.GetElement(1));
            elems.Add(Element.GetElement(2));
            elems.Add(Element.GetElement(5));

            _orbs.SetElements(elems);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _orbs._successCast = true;
        }
    }
}
