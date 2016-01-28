using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementNode{

	protected Dictionary<Element,SpellNode> _nodes;

	private static ElementNode Node;

	public static ElementNode GetInstance(){
		if (Node == null)
			Node = new ElementNode ();

		return Node;
	}

	private ElementNode():this(Element.GetElement (0)){
		
	}

	protected ElementNode(Element element){
		_nodes = new Dictionary<Element, SpellNode> ();
		foreach (var elem in Element.GetElements()) {
			if (element._id <= elem._id) {
				_nodes [elem] = null;
			}
		}
	}

	public SelfSpell GetSelfSpell(ref Queue<Element> elements){
		if(elements.Count==0)
			return null;
		
		Element element = elements.Dequeue ();

		if (!_nodes.ContainsKey (element))
			return null;
		
		if (_nodes [element] == null) {
			return null;
		}
		else {
			return _nodes [element].GetSelfSpell (ref elements);
		}
	}

	public TargetSpell GetTargetSpell(ref Queue<Element> elements){
		if(elements.Count==0)
			return null;

		Element element = elements.Dequeue ();

		if (!_nodes.ContainsKey (element))
			return null;
		
		if (_nodes [element] == null) {
			return null;
		}
		else {
			return _nodes [element].GetTargetSpell(ref elements);
		}
	}

	public void SetSelfSpell(ref SelfSpell selfSpell, ref Queue<Element> elements){
		if (elements.Count == 0)
			return;

		Element element = elements.Dequeue ();

		if (!_nodes.ContainsKey (element))
			return;

		if (_nodes [element] == null) {
			_nodes [element]= new SpellNode(element);
		}

		_nodes [element].SetSelfSpell (ref selfSpell, ref elements);
		
	}

	public void SetTargetSpell(ref TargetSpell targetSpell, ref Queue<Element> elements){
		if (elements.Count == 0)
			return;

		Element element = elements.Dequeue ();

		if (!_nodes.ContainsKey (element))
			return;

		if (_nodes [element] == null) {
			_nodes [element]= new SpellNode(element);
		}

		_nodes [element].SetTargetSpell (ref targetSpell, ref elements);
	}


	protected class SpellNode: ElementNode
	{
		public SpellNode (Element element):base(element){}

		public SelfSpell _selfSpell;

		public TargetSpell _targetSpell;


		new public void SetSelfSpell(ref SelfSpell selfSpell, ref Queue<Element> elements){
			if (elements.Count == 0)
				_selfSpell = selfSpell;

			Element element = elements.Dequeue ();

			if (!_nodes.ContainsKey (element))
				return;


			if (_nodes [element] == null) {
				_nodes [element]= new SpellNode(element);
			}

			_nodes [element].SetSelfSpell (ref selfSpell, ref elements);

		}

		new public void SetTargetSpell(ref TargetSpell targetSpell, ref Queue<Element> elements){
			if (elements.Count == 0)
				_targetSpell = targetSpell;

			Element element = elements.Dequeue ();

			if (!_nodes.ContainsKey (element))
				return;

			if (_nodes [element] == null) {
				_nodes [element]= new SpellNode(element);
			}

			_nodes [element].SetTargetSpell (ref targetSpell, ref elements);
		}
	}
		
}
