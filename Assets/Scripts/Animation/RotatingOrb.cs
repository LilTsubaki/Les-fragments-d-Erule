using UnityEngine;
using System.Collections;

public class RotatingOrb : MonoBehaviour {

    public float _rotationSpeed;
	private Vector3 RandomDirection1;
	private Vector3 RandomDirection2;
	public float Vitesse;

	void Start() {
		Vector3 Rand1 = new Vector3 (EruleRandom.RangeValue (0f, 1f), EruleRandom.RangeValue (0f, 1f), EruleRandom.RangeValue (0f, 1f));
		Vector3 Rand2 = new Vector3 (EruleRandom.RangeValue (0f, 1f), EruleRandom.RangeValue (0f, 1f), EruleRandom.RangeValue (0f, 1f));
		RandomDirection1 = Rand1.normalized;
		RandomDirection2 = Rand2.normalized;
	}

	// Update is called once per frame
	void Update () {
        gameObject.transform.RotateAround(gameObject.transform.parent.position, gameObject.transform.right, _rotationSpeed);
		gameObject.transform.GetChild (0).Rotate((Mathf.Sin(Time.time* Vitesse) * RandomDirection1 + (1 - Mathf.Sin(Time.time* Vitesse)) * RandomDirection2).normalized);
	}
}
