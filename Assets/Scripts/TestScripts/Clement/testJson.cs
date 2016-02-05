using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testJson : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Logger.logLvl = Logger.Type.TRACE;

        SpellManager.getInstance();
        Logger.Trace("spellManager initialized");

        Queue<Element> elements = new Queue<Element>();
        elements.Enqueue(Element.GetElement(3));
        elements.Enqueue(Element.GetElement(3));

        SelfSpell testSp = SpellManager.getInstance().ElementNode.GetSelfSpell(elements);
        Logger.Trace(testSp._effects.GetIds().Count);
        List<int> effectIds = testSp._effects.GetIds();

        for(int i =0; i < effectIds.Count; i++)
        {
            Effect effectTest = SpellManager.getInstance().getDirectEffectById((uint)effectIds[i]);
            
        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
