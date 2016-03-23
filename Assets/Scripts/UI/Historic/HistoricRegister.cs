using UnityEngine;
using System.Collections;

public class HistoricRegister : MonoBehaviour {

    public GameObject content;
    public GameObject texts;
    HistoricManager manager;
    // Use this for initialization
    void Start()
    {
        manager = HistoricManager.GetInstance();
        manager.Init(content, texts, 20);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
