using UnityEngine;
using System.Collections;

public class RotatingOrb : MonoBehaviour {

    public float _rotationSpeed;

	// Update is called once per frame
	void Update () {
        gameObject.transform.RotateAround(gameObject.transform.parent.position, gameObject.transform.right, _rotationSpeed);
	}
}
