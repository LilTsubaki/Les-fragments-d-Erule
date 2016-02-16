using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestReseau : MonoBehaviour {



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void tryingToDoSpell()
    {
        ClientManager.GetInstance()._client.SendMakeSpell();        
    }

}
