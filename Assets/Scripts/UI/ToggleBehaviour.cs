using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleBehaviour : MonoBehaviour
{

    void Awake()
    {
        UIManager.GetInstance().ToggleButton = gameObject.GetComponent<Button>();
        gameObject.GetComponentInChildren<Text>().text = "Lock Spell";
    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (!ClientManager.GetInstance()._client.LockedMode)
        {
            gameObject.GetComponentInChildren<Text>().text = "Lock Spell";
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Unlock Spell";
        }
    }

    public void SwitchMode()
    {
        if(ClientManager.GetInstance()._client.IsMyTurn)
        {
            
           
             if (!ClientManager.GetInstance()._client.LockedMode)
            {
                ClientManager.GetInstance()._client.SendMovementMode();
            }
            else
            {
                ClientManager.GetInstance()._client.SendMakeSpell();
            }

            ClientManager.GetInstance()._client.LockedMode = !ClientManager.GetInstance()._client.LockedMode;
        }   
    }
}
