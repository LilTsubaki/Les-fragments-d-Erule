using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleBehaviour : MonoBehaviour
{

    void Awake()
    {
        UIManager.GetInstance().ToggleButton = gameObject.GetComponent<Button>();
        gameObject.GetComponentInChildren<Text>().text = "Lancer le sort";
    }


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!ClientManager.GetInstance()._client.LockedMode)
        {
            gameObject.GetComponentInChildren<Text>().text = "Lancer le sort";
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = "Annuler";
        }
    }

    public void SwitchMode()
    {
        if(ClientManager.GetInstance()._client.IsMyTurn)
        {
            
            ClientManager.GetInstance()._client.LockedMode = !ClientManager.GetInstance()._client.LockedMode;
           
             if (!ClientManager.GetInstance()._client.LockedMode)
            {
                ClientManager.GetInstance()._client.SendMovementMode();
            }
            else
            {
                ClientManager.GetInstance()._client.SendMakeSpell();
            }

            AudioManager.GetInstance().Play("lockSpell", true, false);

        }
        else
        {
            AudioManager.GetInstance().Play("error", true, false);
        }
    }
}
