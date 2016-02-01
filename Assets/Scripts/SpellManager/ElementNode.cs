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
	public SelfSpell GetSelfSpell(Queue<Element> elements){
		if(elements.Count==0)
			return null;

        Element element = elements.Dequeue ();
        Logger.Trace(element._name);

        if (!_nodes.ContainsKey (element))
			return null;
		
		if (_nodes [element] == null) {
			return null;
		}
		else {
			return _nodes [element].GetSelfSpell (elements);
		}
	}

	/// <summary>
	/// Gets the target spell.
	/// </summary>
	/// <returns>The target spell.</returns>
	/// <param name="elements">Elements.</param>
	public TargetSpell GetTargetSpell(Queue<Element> elements){
		if(elements.Count==0)
			return null;

        Element element = elements.Dequeue ();
        Logger.Trace(element._name);

        if (!_nodes.ContainsKey (element))
			return null;
		
		if (_nodes [element] == null) {
			return null;
		}
		else {
			return _nodes [element].GetTargetSpell(elements);
		}
	}

	/// <summary>
	/// Sets the self spell.
	/// </summary>
	/// <param name="selfSpell">Self spell.</param>
	/// <param name="elements">Elements.</param>
	public void SetSelfSpell(ref SelfSpell selfSpell, Queue<Element> elements){
		if (elements.Count == 0)
			return;

        Element element = elements.Dequeue ();
        Logger.Trace(element._name);


        if (!_nodes.ContainsKey (element))
			return;

		if (_nodes [element] == null) {
			_nodes [element]= new SpellNode(element);
		}

		_nodes [element].SetSelfSpell (ref selfSpell,elements);
		
	}

	/// <summary>
	/// Sets the target spell.
	/// </summary>
	/// <param name="targetSpell">Target spell.</param>
	/// <param name="elements">Elements.</param>
	public void SetTargetSpell(ref TargetSpell targetSpell,Queue<Element> elements){
		if (elements.Count == 0)
			return;

        Element element = elements.Dequeue ();
        Logger.Trace(element._name);

        if (!_nodes.ContainsKey (element))
			return;

		if (_nodes [element] == null) {
			_nodes [element]= new SpellNode(element);
		}

		_nodes [element].SetTargetSpell (ref targetSpell, elements);
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
		new public void SetSelfSpell(ref SelfSpell selfSpell, Queue<Element> elements){
			if (elements.Count == 0)
            {
                _selfSpell = selfSpell;
                return;
            }
				

			Element element = elements.Dequeue ();
            Logger.Trace(element._name);

            if (!_nodes.ContainsKey (element))
				return;


			if (_nodes [element] == null) {
				_nodes [element]= new SpellNode(element);
			}

			_nodes [element].SetSelfSpell (ref selfSpell, elements);

		}

		/// <summary>
		/// Sets the target spell.
		/// </summary>
		/// <param name="targetSpell">Target spell.</param>
		/// <param name="elements">Elements.</param>
		new public void SetTargetSpell(ref TargetSpell targetSpell, Queue<Element> elements){
			if (elements.Count == 0)
            {
                _targetSpell = targetSpell;
                return;
            }
				

            Element element = elements.Dequeue ();
            Logger.Trace(element._name);

            if (!_nodes.ContainsKey (element))
				return;

			if (_nodes [element] == null) {
				_nodes [element]= new SpellNode(element);
			}

			_nodes [element].SetTargetSpell (ref targetSpell, elements);
		}

        /// <summary>
        /// Gets the self spell.
        /// </summary>
        /// <returns>The self spell.</returns>
        /// <param name="elements">Elements.</param>
        new public SelfSpell GetSelfSpell(Queue<Element> elements)
        {
            if (elements.Count == 0)
                return _selfSpell;

            Element element = elements.Dequeue();
            Logger.Trace(element._name);

            if (!_nodes.ContainsKey(element))
                return null;

            if (_nodes[element] == null)
            {
                return null;
            }
            else {
                return _nodes[element].GetSelfSpell(elements);
            }
        }

        /// <summary>
        /// Gets the target spell.
        /// </summary>
        /// <returns>The target spell.</returns>
        /// <param name="elements">Elements.</param>
        new public TargetSpell GetTargetSpell(Queue<Element> elements)
        {
            if (elements.Count == 0)
                return _targetSpell;

            Element element = elements.Dequeue();
            Logger.Trace(element._name);

            if (!_nodes.ContainsKey(element))
                return null;

            if (_nodes[element] == null)
            {
                return null;
            }
            else {
                return _nodes[element].GetTargetSpell(elements);
            }
        }
    }
		
}
