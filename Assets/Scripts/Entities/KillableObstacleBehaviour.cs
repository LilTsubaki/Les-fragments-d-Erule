using UnityEngine;
using System.Collections;

public class KillableObstacleBehaviour : MonoBehaviour
{
    private KillableObstacle _killableObstacle;

    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(_killableObstacle.isDead())
        {
            _killableObstacle.Position._entity = null;
            Destroy(gameObject);
        }
	}

    public KillableObstacle KillableObstacle
    {
        get
        {
            return _killableObstacle;
        }

        set
        {
            _killableObstacle = value;
        }
    }
}
