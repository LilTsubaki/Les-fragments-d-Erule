using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerShard : Obstacle
{

    private int _cooldown;
    private int _currentCooldown;
    private List<int> _effectIds;
    public PowerShard(Hexagon position, GameObject gameObject, int cooldown, List<int> effectIds) : base(position)
    {
        _cooldown = cooldown;
        _currentCooldown = 0;
        _effectIds = effectIds;
    }

    public bool isReady()
    {
        return _currentCooldown >= 0;
    }

    public bool UpdateCooldown()
    {
        _currentCooldown--;
        return isReady();
    }

    public void ApplyEffect()
    {
        if (isReady())
        {
            List<Hexagon> hexas = Position.GetNeighbours();
            bool apply = false;
            Effect effect = SpellManager.getInstance().GetDirectEffectById(GetRandomEffect());
            foreach (var hexa in hexas)
            {
                if (hexa._entity != null && hexa._entity is Character)
                {
                    effect.ApplyEffect(hexas, hexa, null);
                    apply = true;
                    break;
                }
            }

            if (apply)
            {
                _currentCooldown = _cooldown;
            }
        }
    }

    private int GetRandomEffect()
    {
        return _effectIds[Random.Range(0, _effectIds.Count - 1)];
    }

}
