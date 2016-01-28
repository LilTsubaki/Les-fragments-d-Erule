using UnityEngine;
using System.Collections;

public class testJson : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        JSONObject js = JSONObject.GetJsonObjectFromFile(Application.dataPath+"/JsonFiles/test.json");
        Effects effects = new Effects(js);

        foreach(var id in effects.GetIds())
        {
            Debug.Log(id);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
