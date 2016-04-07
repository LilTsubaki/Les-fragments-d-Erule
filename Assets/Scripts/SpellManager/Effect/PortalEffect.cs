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
            if (target.Portal != null)
            {
                target.Portal.Destroy();
            }
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

        HistoricManager.GetInstance().AddText(String.Format(StringsErule.portal, caster.Name));
    }
}
