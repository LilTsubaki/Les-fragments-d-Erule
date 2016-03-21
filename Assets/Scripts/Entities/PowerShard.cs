using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerShard : Obstacle
{

    private int _cooldown;
    private int _currentCooldown;
    private List<int> _effectIds;
    public PowerShard(Hexagon position, int cooldown, List<int> effectIds) : base(position)
    {
        _cooldown = cooldown;
        _currentCooldown = 0;
        _effectIds = effectIds;
    }

    public bool isReady()
    {
        return _currentCooldown <= 0;
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
            List<Hexagon> hexas = Position.GetAllNeighbours();
            bool apply = false;
           
            foreach (var hexa in hexas)
            {
                if (hexa._entity != null && hexa._entity is Character && PlayBoardManager.GetInstance().isMyTurn((Character)hexa._entity))
                {
                    Effect effect = SpellManager.getInstance().GetDirectEffectById(GetRandomEffect());
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
        return _effectIds[EruleRandom.RangeValue(0, _effectIds.Count - 1)];
    }

    public JSONObject PowerShardToJSON()
    {
        JSONObject powerShard = new JSONObject(JSONObject.Type.OBJECT);
        JSONObject arr = new JSONObject(JSONObject.Type.ARRAY);
        powerShard.AddField("coolDown", _cooldown);
        powerShard.AddField("effectIds", arr);
        powerShard.AddField("gameObject", GameObject.name);

        for (int i = 0; i < _effectIds.Count; i++)
        {
            arr.Add(_effectIds[i]);
        }

        return powerShard;
    }

    public static PowerShard JSONToPowerShard(JSONObject powerShard, Hexagon position)
    {
        int coolDown=(int)powerShard.GetField("coolDown").n;
        List<JSONObject> ids = powerShard.GetField("effectIds").list;
        List<int> effectIds = new List<int>();

        foreach(JSONObject jo in ids)
        {
            effectIds.Add((int)jo.n);
        }
        PowerShard ps = new PowerShard(position, coolDown, effectIds);
        ps.GameObject = new GameObject();
        CapsuleCollider collider = ps.GameObject.AddComponent<CapsuleCollider>();
        collider.height = 5;
        collider.radius = 0.5f;
        ps.GameObject.name = powerShard.GetField("gameObject").str;
        return ps;
    }
}
