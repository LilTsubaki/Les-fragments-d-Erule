using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIKillableObstacle : MonoBehaviour {

    KillableObstacle _obstacle;
    public ProgressionBar _life;

    public KillableObstacle Obstacle
    {
        get
        {
            return _obstacle;
        }

        set
        {
            _obstacle = value;
            SetSliderValues();
        }
    }

	// Update is called once per frame
	void Update () {
        if(Obstacle != null)
            _life._value = Obstacle.CurrentLife;
	}

    public void SetSliderValues()
    {
        _life._maxValue = Obstacle.MaxLife;
    }
}
