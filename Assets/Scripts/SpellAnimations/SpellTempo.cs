using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellTempo : MonoBehaviour {

    private Vector3 _from;
    private Vector3 _to;

    private List<Hexagon> _hexagons;

    public float _fireTempo;
    public float _waterTempo;
    public float _airTempo;
    public float _earthTempo;
    public float _woodTempo;
    public float _metalTempo;

    // Use this for initialization
    void Start () {
        SpellAnimationManager.GetInstance().RegisterTempo(this);
	}
	
    public void Init(Vector3 from, Vector3 to, List<Hexagon> hexagons)
    {
        _from = from;
        _to = to;
        _hexagons = hexagons;
    }

    public void PlayLater(string anim, float time)
    {
        Invoke(anim, time);
    }

    private void Fire()
    {
        SpellAnimationManager.GetInstance().Play("fire", _from, _to, _hexagons);
    }
    private void Air()
    {
        SpellAnimationManager.GetInstance().Play("air", _from, _to, _hexagons);
    }
    private void Water()
    {
        SpellAnimationManager.GetInstance().Play("water", _from, _to, _hexagons);
    }
    private void Earth()
    {
        SpellAnimationManager.GetInstance().Play("earth", _from, _to, _hexagons);
    }
    private void Wood()
    {
        SpellAnimationManager.GetInstance().Play("wood", _from, _to, _hexagons);
    }
    private void Metal1()
    {
        SpellAnimationManager.GetInstance().Play("metal1", _from, _to, _hexagons);
    }
    private void Metal2()
    {
        SpellAnimationManager.GetInstance().Play("metal2", _from, _to, _hexagons);
    }
    private void Metal3()
    {
        SpellAnimationManager.GetInstance().Play("metal3", _from, _to, _hexagons);
    }
}
