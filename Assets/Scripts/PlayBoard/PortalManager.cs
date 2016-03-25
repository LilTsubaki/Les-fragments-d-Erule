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
            _instance.Init();
        }

        return _instance;
    }

    private void Init()
    {
        GameObject portal1Prefab = Resources.Load<GameObject>("prefabs/Portal_1");
        GameObject portal1GO = GameObject.Instantiate(portal1Prefab);
        GameObject portal2Prefab = Resources.Load<GameObject>("prefabs/Portal_2");
        GameObject portal2GO = GameObject.Instantiate(portal2Prefab);

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

        if (TwoPortalsActivated())
        {
            if (_portal1.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture == null)
            {
                _portal1.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<RenderTexture>("images/TextureFromPortal2");
            }

            if (_portal2.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture == null)
            {
                _portal2.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<RenderTexture>("images/TextureFromPortal1");
            }
        }
        else
        {
            if(_portal1.IsActive())
            {
                _portal1.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = null;
            }

            if (_portal2.IsActive())
            {
                _portal2.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = null;
            }
        }
    }

    public bool TwoPortalsActivated()
    {
        return _portal1.IsActive() && _portal2.IsActive();
    }

    public void Teleport(Character character)
    {
        Logger.Debug("2 portals ? " +TwoPortalsActivated());
        Logger.Debug("Char on portal ? " + (character.Position.Portal != null));
        if (TwoPortalsActivated() && character.Position.Portal != null)
        {
            if (character.Position.Portal == _portal1)
            {
                if (_portal2.Position._entity == null)
                {
                    Logger.Debug("On portal 1, portal 2 has no entity");
                    character.Position = _portal2.Position;
                    _portal1.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = null;
                    _portal2.Destroy();
                }
            }
            else
            {
                if(_portal1.Position._entity == null)
                {
                    Logger.Debug("On portal 2, portal 1 has no entity");
                    character.Position = _portal1.Position;
                    _portal2.GameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = null;
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
