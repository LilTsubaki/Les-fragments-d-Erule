using UnityEngine;
using System.Collections.Generic;

public class TestCastAnimation : MonoBehaviour {

    public GameObject _cube1;
    public GameObject _cube2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // UNDERWHELMING
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpellAnimationManager.GetInstance().Play("fire", _cube1, _cube2);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpellAnimationManager.GetInstance().Play("air", _cube1, _cube2);
        }
		if (Input.GetKeyDown(KeyCode.E))
		{
			SpellAnimationManager.GetInstance().Play("water", _cube1, _cube2);
		}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpellAnimationManager.GetInstance().Play("metal1", _cube1, _cube2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpellAnimationManager.GetInstance().Play("metal2", _cube1, _cube2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpellAnimationManager.GetInstance().Play("metal3", _cube1, _cube2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            SpellAnimationManager.GetInstance().PlayList(elements, _cube1, _cube2);
        }
    }
}
