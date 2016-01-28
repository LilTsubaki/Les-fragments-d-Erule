using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementNode{

	private Dictionary<Element,ElementNode> _nodes;

	public ElementNode():this(Element.GetElement (0)){}

	public ElementNode(Element element){
		_nodes = new Dictionary<Element, ElementNode> ();
		foreach (var elem in Element.GetElements()) {
			if (element._id <= elem._id) {
				_nodes [elem] = null;
			}
		}
	}

	public SelfSpell GetSelfSpell(List<Element> elements){
		//TODO
		return null;
	}

	public TargetSpell GetTargetSpell(List<Element> elements){
		//TODO
		return null;
	}

	public void SetSelfSpell(SelfSpell selfSpell, List<Element> elements){
		//TODO
	}

	public void SetTargetSpell(TargetSpell targetSpell, List<Element> elements){
		//TODO
	}
		
}
