using UnityEngine;
using System.Collections;
using Xft;

public class MetalAnimation : SpellAnimation
{
    public Animator _anim;
    public RotatoryAxe _axe;
    public XWeaponTrail _trail;
    public XWeaponTrail _trailDistort;

    // Update is called once per frame
    void Update () {

        if (_play)
        {
            AudioManager.GetInstance().Play("spellMetalSwing", true, false);
            _axe.RandomInclination();
            Reset();
            gameObject.transform.position = _to + new Vector3(0,.6f,0);
            
            _anim.SetTrigger("play");
            _play = !_play;
            

        }
        if (_updateTimer)
        {
            timerUpdate();
        }
    }

    public override void Reset()
    {
        transform.position = _from;
        Vector3 look = new Vector3(_to.x, _from.y, _to.z);
        gameObject.transform.LookAt(look);
        _trail.Deactivate();
        _trailDistort.Deactivate();
        _trail.Activate();
        _trailDistort.Activate();
    }
}
