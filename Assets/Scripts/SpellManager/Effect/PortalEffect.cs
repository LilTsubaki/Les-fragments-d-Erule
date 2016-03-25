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
        PortalManager portalManager = PortalManager.GetInstance();

        if (target._entity == null)
        {
            portalManager.CreatePortal(target);
        }
        else
        {
            Logger.Debug("PortalEffect : Targeted hexagon has an entity");
        }

        // Automatically teleport players that stand on portals
        if (portalManager.TwoPortalsActivated())
        {
            if (portalManager.Portal1.Position._entity is Character)
            {
                Character character = (Character)portalManager.Portal1.Position._entity;
                portalManager.Teleport(character);
            }
            else if (portalManager.Portal2.Position._entity is Character)
            {
                Character character = (Character)portalManager.Portal2.Position._entity;
                portalManager.Teleport(character);
            }
        }

        /*List<Portal> portals = PlayBoardManager.GetInstance().Board.Portals;
        if (target._entity == null)
        {
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
        }

        // Automatically teleport players that stand on portals
        if (portals.Count >= 2)
        {
            Character character = new Character(0);
            bool canTeleport = false;
            for (int i = 0; i < portals.Count; i++)
            {
                if (portals[i].Position._entity is Character)
                {
                    character = (Character)portals[i].Position._entity;
                    canTeleport = true;
                    break;
                }
            }
            if (canTeleport)
            {
                character.Teleport();
            }
        }*/
        HistoricManager.GetInstance().AddText(String.Format(StringsErule.portal, caster.Name));
    }
}
