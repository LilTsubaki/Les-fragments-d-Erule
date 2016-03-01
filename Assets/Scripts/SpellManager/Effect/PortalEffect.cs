using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PortalEffect : EffectDirect
{
    public PortalEffect(int id) : base(id)
    {

    }

    public PortalEffect(JSONObject js) : base()
    {
        _id = (int)js.GetField("id").n;
    }

    public override void ApplyEffect(List<Hexagon> hexagons, Hexagon target, Character caster)
    {
        if (target._entity == null)
        {
            List<Portal> portals = PlayBoardManager.GetInstance().Board.Portals;
            if (portals.Count >= 2)
            {
                portals[0].Destroy();
                portals.RemoveAt(0);
            }
            GameObject prefab = (GameObject)Resources.Load("Prefabs/Portal", typeof(GameObject));
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.position = target.GameObject.transform.position;
            Portal portal = new Portal(target, instance);
            portals.Add(portal);
            Logger.Debug("Nombre portails : portals.Count");
        }
        else
        {
            Logger.Debug("Targeted hexagon has an entity");
        }
    }
}
