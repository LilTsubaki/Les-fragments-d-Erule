using UnityEngine;
using System.Collections;

public class SelfdestructParticles : MonoBehaviour {

    private ParticleSystem _system;

	// Use this for initialization
	void Start () {
        _system = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_system)
        {
            if (!_system.IsAlive())
            {
                Destroy(gameObject);
            }
        }
	
	}
}
