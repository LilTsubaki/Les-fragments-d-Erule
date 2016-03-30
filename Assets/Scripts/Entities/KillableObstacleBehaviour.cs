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
            EffectUIManager.GetInstance().DeleteEntity(_killableObstacle);
            if (!EffectUIManager.GetInstance().Contains(_killableObstacle)) {
                _killableObstacle.Position._entity = null;
                Destroy(gameObject);
                PlayBoardManager.GetInstance().Board._colorAccessible = true;
            }
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
