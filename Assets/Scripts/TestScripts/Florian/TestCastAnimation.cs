using UnityEngine;
using System.Collections.Generic;

public class TestCastAnimation : MonoBehaviour {

    public GameObject _castFrom;
    public GameObject _castTo;

    private Character _char1;
    private Character _char2;

    // Use this for initialization
    void Start () {
        _char1 = new Character(1);
        _char1.GameObject = _castFrom;
        _castFrom.GetComponent<CharacterBehaviour>()._character = _char1;
        _char2 = new Character(1);
        _char2.GameObject = _castTo;
        _castTo.GetComponent<CharacterBehaviour>()._character = _char2;
    }
	
	// Update is called once per frame
	void Update () {
        // UNDERWHELMING
        if (Input.GetKeyDown(KeyCode.F))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(0));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(2));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
		if (Input.GetKeyDown(KeyCode.E))
		{
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(1));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(4));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(3));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(5));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castFrom.transform.position, new List<Hexagon>());
            SpellAnimationManager.GetInstance().PlayCharacterAnimationSelf(elements, _char1);
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(1));
            elements.Add(Element.GetElement(1));
            elements.Add(Element.GetElement(1));
            elements.Add(Element.GetElement(1));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castFrom.transform.position, new List<Hexagon>());
            SpellAnimationManager.GetInstance().PlayCharacterAnimationSelf(elements, _char1);
            _char1.SetOrbs(elements);
            _castFrom.GetComponent<CharacterBehaviour>()._orbs._successCast = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpellAnimationManager.GetInstance().Play("metal1", _castFrom.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpellAnimationManager.GetInstance().Play("metal2", _castFrom.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpellAnimationManager.GetInstance().Play("metal3", _castFrom.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            SpellAnimationManager.GetInstance().PlayList(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpellAnimationManager.GetInstance().Play("selfHalo", _castTo.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpellAnimationManager.GetInstance().Play("selfParticles", _castTo.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpellAnimationManager.GetInstance().Play("selfCircle", _castTo.transform.position, _castTo.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {


            List<Element> elements = new List<Element>();
            elements.Add(Element.GetElement(0));
            elements.Add(Element.GetElement(1));
            elements.Add(Element.GetElement(2));
            elements.Add(Element.GetElement(3));
            elements.Add(Element.GetElement(4));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            elements.Add(Element.GetElement(5));
            _char1.NextState = Character.State.CastingSpell;
            SpellAnimationManager.GetInstance().PlayCharacterAnimation(elements, _castFrom.GetComponent<CharacterBehaviour>()._character);
            SpellAnimationManager.GetInstance().SaveCast(elements, _castFrom.transform.position, _castTo.transform.position, new List<Hexagon>());
        }
    }
}
