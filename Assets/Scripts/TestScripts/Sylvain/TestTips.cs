using UnityEngine;
using System.Collections;

public class TestTips : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Logger.Debug(EruleTips.GetInstance().GetRandomTip());
	}
}
