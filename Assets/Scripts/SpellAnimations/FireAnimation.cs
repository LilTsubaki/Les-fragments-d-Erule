using UnityEngine;
using System.Collections;

public class FireAnimation : SpellAnimation
{
    public Animator _fireAnimator;
    public float _forward;

    void Update()
    {
        if (_play)
        {
            Vector3 projCam = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);
            gameObject.transform.LookAt(projCam);
            gameObject.transform.position = _to.transform.position + gameObject.transform.forward * _forward;
            _fireAnimator.SetTrigger("play");
            _play = !_play;
        }
    }
}
