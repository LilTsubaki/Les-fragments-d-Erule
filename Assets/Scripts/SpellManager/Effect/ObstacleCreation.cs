using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;


public class ObstacleCreation : EffectDirect
{
    private int _life;
    private GameObject _prefab;
    private string _name;

    public ObstacleCreation(JSONObject js)
    {
        _id = (int)js.GetField("id").n;
        Life = (int)js.GetField("life").n;
        Name = js.GetField("name").str;
        _prefab = (GameObject)Resources.Load("Prefabs/" + Name, typeof(GameObject));
    }

    

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        Logger.Debug("obstacle creation");
        Logger.Debug(hexagons.Count);
        foreach (Hexagon hexa in hexagons)
        {
            if(hexa._entity == null)
            {
                KillableObstacle obs = new KillableObstacle(hexa, _life);
                obs._gameobject = GameObject.Instantiate(_prefab);
                KillableObstacleBehaviour kob = obs._gameobject.AddComponent<KillableObstacleBehaviour>();
                kob.KillableObstacle = obs;
                obs._gameobject.transform.parent = hexa.GameObject.transform;
                obs._gameobject.name = Name;
                obs._gameobject.transform.position = new Vector3(0.866f * hexa._posX - 0.433f * hexa._posY, hexa.GameObject.transform.position.y, 0.75f * hexa._posY);
                obs._gameobject.layer = LayerMask.NameToLayer("Obstacle");
                GameObject lifeCanvas = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasObstacle") as GameObject);
                lifeCanvas.transform.SetParent(obs._gameobject.transform);
                lifeCanvas.transform.localPosition = new Vector3(0, .6f, 0);
                lifeCanvas.GetComponent<UIKillableObstacle>().Obstacle = obs;
            }
        }
    }

    public int Life
    {
        get
        {
            return _life;
        }

        set
        {
            _life = value;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return _prefab;
        }

        set
        {
            _prefab = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }
}

