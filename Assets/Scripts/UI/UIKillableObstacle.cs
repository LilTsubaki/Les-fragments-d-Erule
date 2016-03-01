using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIKillableObstacle : MonoBehaviour {

    KillableObstacle _obstacle;
    public Slider _life;

    public KillableObstacle Obstacle
    {
        get
        {
            return _obstacle;
        }

        set
        {
            _obstacle = value;
        }
    }

	// Update is called once per frame
	void Update () {
        if(Obstacle != null)
            _life.value = Obstacle.CurrentLife;
	}

    public void SetSliderValues()
    {
        _life.maxValue = Obstacle.MaxLife;
    }
}
