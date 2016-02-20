using UnityEngine;
using System.Collections;

public class EffectUIBehaviour : MonoBehaviour {

    public GameObject _textEffectUI;
    public GameObject _textEffectParent;
    
	// Use this for initialization
	void Start () {
        EffectUIManager manager = EffectUIManager.GetInstance();
        manager.Init(_textEffectUI, _textEffectParent);

        Character test = new Character(4000);
        manager.RegisterCharacter(test, 1.0f);

        manager.AddTextEffect(test, new TextDamage(200, Element.GetElement(0)));
        manager.AddTextEffect(test, new TextDamage(100, Element.GetElement(1)));

    }
	
	// Update is called once per frame
	void Update () {
        EffectUIManager.GetInstance().Update();
	}
}
