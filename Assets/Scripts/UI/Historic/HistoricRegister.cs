using UnityEngine;
using System.Collections;

public class HistoricRegister : MonoBehaviour {

    public GameObject content;
    public GameObject texts;
    public int maxMesage;
    HistoricManager manager;
    // Use this for initialization
    void Start()
    {
        manager = HistoricManager.GetInstance();
        manager.Init(content, texts, maxMesage);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
