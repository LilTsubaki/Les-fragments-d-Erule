using UnityEngine;
using System.Collections;

public class LoggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Logger.logLvl = Logger.Type.DEBUG;
		Logger.Warning ("coucou");
		Logger.Error ("coucou");
		Logger.Debug ("coucou");
		Logger.Trace ("coucou");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
