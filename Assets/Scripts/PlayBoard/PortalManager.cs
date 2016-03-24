using UnityEngine;
using System.Collections;

public class PortalManager {

    private static PortalManager _instance;

    private Portal _portal1;
    private Portal _portal2;

    public static PortalManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PortalManager();
        }

        return _instance;
    }

    public void Init()
    {
        GameObject portal1GO = Resources.Load<GameObject>("prefabs/Portal_1");
        GameObject portal2GO = Resources.Load<GameObject>("prefabs/Portal_2");
        _portal1 = new Portal(null, portal1GO, 1);
        _portal1.GameObject.SetActive(false);
        _portal2 = new Portal(null, portal2GO, 2);
        _portal2.GameObject.SetActive(false);
    }

    public void CreatePortal(Hexagon position)
    {
        if (!_portal1.IsActive())
        {
            _portal1.ActivatePortal(position);
        }
        else if (!_portal2.IsActive())
        {
            _portal2.ActivatePortal(position);
        }
        else
        {
            if(_portal1.Timestamp > _portal2.Timestamp)
            {
                _portal2.ActivatePortal(position);
            }
            else
            {
                _portal1.ActivatePortal(position);
            }
        }
    }

    public bool TwoPortalsActivated()
    {
        return _portal1.IsActive() && _portal2.IsActive();
    }

    public void Teleport(Character character)
    {
        if (TwoPortalsActivated() && character.Position.Portal != null)
        {
            if (character.Position.Portal == _portal1)
            {
                if (_portal2.Position._entity != null)
                {
                    character.Position = _portal2.Position;
                    _portal2.Destroy();
                }
            }
            else
            {
                if(_portal1.Position._entity != null)
                {
                    character.Position = _portal1.Position;
                    _portal1.Destroy();
                }
            }
            character.GameObject.transform.position = character.Position.GameObject.transform.position + character.PositionOffset;
        }
    }
    
    public Portal Portal1
    {
        get
        {
            return _portal1;
        }

        set
        {
            _portal1 = value;
        }
    }

    public Portal Portal2
    {
        get
        {
            return _portal2;
        }

        set
        {
            _portal2 = value;
        }
    }
}
