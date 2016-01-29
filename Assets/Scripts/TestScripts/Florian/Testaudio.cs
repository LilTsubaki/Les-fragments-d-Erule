using UnityEngine;
using System.Collections;

public class Testaudio : MonoBehaviour {

    int idMusic;

	// Use this for initialization
	void Start () {
        idMusic = -1;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            idMusic = AudioManager.GetInstance().Play("music", true);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioManager.GetInstance().Mute();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.GetInstance().FadeIn(idMusic);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.GetInstance().FadeOut(idMusic);
        }

	}
}
