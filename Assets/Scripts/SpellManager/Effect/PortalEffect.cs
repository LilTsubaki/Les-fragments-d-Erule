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

            // Automatically teleport the caster if is on a Portal.
            if (caster.Position.Portal != null)
            {
                caster.Teleport();
                caster._gameObject.transform.position = caster.Position.GameObject.transform.position + caster.PositionOffset;
            }
        }
        else
        {
            Logger.Debug("PortalEffect : Targeted hexagon has an entity");
        }
    }
}
