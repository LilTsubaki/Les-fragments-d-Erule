using UnityEngine;
using System.Collections;

public class TestMusic : MonoBehaviour {

    private int a;
    public ProceduralMusic _music;
    public ProceduralMusic _music2;
    public ProceduralMusic _music3;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.GetInstance().PlayMusic("MusicBattleFull");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.GetInstance().StopMusic("MusicBattleFull");
            AudioManager.GetInstance().Play("MusicTransition");
            AudioManager.GetInstance().PlayMusic("MusicBattleHalf");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.GetInstance().StopMusic("MusicBattleHalf");
            AudioManager.GetInstance().Play("MusicTransition");
            AudioManager.GetInstance().PlayMusic("MusicBattleEnd");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.GetInstance().StopMusic("MusicBattleEnd");
            AudioManager.GetInstance().Play("MusicTransition");
            AudioManager.GetInstance().Play("MusicVictory");
        }

    }
}
