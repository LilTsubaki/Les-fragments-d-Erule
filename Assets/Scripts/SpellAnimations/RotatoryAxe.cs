using UnityEngine;
using System.Collections;

public class RotatoryAxe : MonoBehaviour {

    public float _rotX;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(_rotX, 0, 0);
	}

    public void RandomInclination()
    {
        gameObject.transform.Rotate(0, 0, EruleRandom.RangeValue(-30, 30));
    }

}
