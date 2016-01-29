using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementNode{

	/// <summary>
	/// The children nodes.
	/// </summary>
	protected Dictionary<Element,SpellNode> _nodes;

	/// <summary>
	/// The root node.
	/// </summary>
	private static ElementNode Node;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <returns>The instance.</returns>
	public static ElementNode GetInstance(){
		if (Node == null)
			Node = new ElementNode ();

		return Node;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ElementNode"/> class.
	/// </summary>
	private ElementNode():this(Element.GetElement (0)){
		
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ElementNode"/> class.
	/// </summary>
	/// <param name="element">Element.</param>
	protected ElementNode(Element element){
		_nodes = new Dictionary<Element, SpellNode> ();
		foreach (var elem in Element.GetElements()) {
			if (element._id <= elem._id) {
				_nodes [elem] = null;
			}
		}
	}

	/// <summary>
	/// Gets the self spell.
	/// </summary>
	/// <returns>The self spell.</returns>
	/// <param name="elements">Elements.</param>
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

	/// <summary>
	/// Gets the target spell.
	/// </summary>
	/// <returns>The target spell.</returns>
	/// <param name="elements">Elements.</param>
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

	/// <summary>
	/// Sets the self spell.
	/// </summary>
	/// <param name="selfSpell">Self spell.</param>
	/// <param name="elements">Elements.</param>
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

	/// <summary>
	/// Sets the target spell.
	/// </summary>
	/// <param name="targetSpell">Target spell.</param>
	/// <param name="elements">Elements.</param>
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


	/// <summary>
	/// Spell node.
	/// </summary>
	protected class SpellNode: ElementNode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ElementNode+SpellNode"/> class.
		/// </summary>
		/// <param name="element">Element.</param>
		public SpellNode (Element element):base(element){}

		/// <summary>
		/// The self spell.
		/// </summary>
		public SelfSpell _selfSpell;

		/// <summary>
		/// The target spell.
		/// </summary>
		public TargetSpell _targetSpell;

		/// <summary>
		/// Sets the self spell.
		/// </summary>
		/// <param name="selfSpell">Self spell.</param>
		/// <param name="elements">Elements.</param>
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

		/// <summary>
		/// Sets the target spell.
		/// </summary>
		/// <param name="targetSpell">Target spell.</param>
		/// <param name="elements">Elements.</param>
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
