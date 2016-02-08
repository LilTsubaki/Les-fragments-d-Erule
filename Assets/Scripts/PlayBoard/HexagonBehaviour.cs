using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonBehaviour : MonoBehaviour
{
    public Hexagon _hexagon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (_hexagon.Targetable)
        {
            //draw area
            _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (_hexagon.Targetable)
        {
            _hexagon.GameObject.GetComponentInChildren<Renderer>().material.color = _hexagon.PreviousColor;
        }
    }
}
