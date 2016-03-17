using UnityEngine;
using System.Collections;

public class RotatoryAxe : MonoBehaviour {

    public float _rotX;
    public GameObject _trail;
	
	// Update is called once per frame
	void Update () {
        if(gameObject.activeInHierarchy)
            gameObject.transform.Rotate(_rotX, 0, 0);
	}

    public void RandomInclination()
    {
        int rotation = EruleRandom.RangeValue(-30, 30);
        gameObject.transform.Rotate(0, 0, rotation);
        _trail.transform.Rotate(0, 0, rotation);
    }

}
