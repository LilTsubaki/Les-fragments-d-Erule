using UnityEngine;
using System.Collections;

public class HistoricTest : MonoBehaviour {

    public string text;
    public GameObject content;
    public GameObject texts;
    public GameObject textPrefab;
    int cpt = 0;
    HistoricManager manager;
    // Use this for initialization
    void Start () {
        manager = HistoricManager.GetInstance();
        manager.Init(content, texts, 20);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            manager.AddText(text+cpt++);
        }
	}
}
