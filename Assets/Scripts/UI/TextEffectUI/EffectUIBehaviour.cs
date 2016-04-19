using UnityEngine;
using System.Collections;

public class EffectUIBehaviour : MonoBehaviour {

    public GameObject _textEffectUI;
    public GameObject _textEffectParent;
    
	// Use this for initialization
	void Start () {
        EffectUIManager manager = EffectUIManager.GetInstance();
        manager.Init(_textEffectUI, _textEffectParent);
        
        /*Character test = new Character(4000);
        manager.RegisterEntity(test);


        manager.AddTextEffect(test, new TextRangeGain(1));
        manager.AddTextEffect(test, new TextRangeLoss(2));
        manager.AddTextEffect(test, new TextResistanceGain(3, Element.GetElement(2)));
        manager.AddTextEffect(test, new TextResistanceLoss(4, Element.GetElement(3)));
        manager.AddTextEffect(test, new TextDamage(200, Element.GetElement(0)));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(1)));
        manager.AddTextEffect(test, new TextHeal(100));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(2)));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(3)));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(4)));
        manager.AddTextEffect(test, new TextHeal(100));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(5)));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(1)));
        
        manager.Unpause(test);*/
    }
	
	// Update is called once per frame
	void Update () {
        EffectUIManager.GetInstance().Update();
	}
}
