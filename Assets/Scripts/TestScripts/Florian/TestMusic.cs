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
            _music.StartPlaying();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _music.Stop();
            AudioManager.GetInstance().Play("transition");
            _music2.StartPlaying();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _music2.Stop();
            AudioManager.GetInstance().Play("transition");
            _music3.StartPlaying();
        }

    }
}
