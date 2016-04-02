using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EarthAnimation : SpellAnimation
{
    private List<HexagonBehaviour> _hexagons;
    private bool _raycastDone;
    private float _currentTime;
    private int _currentIndex;

    [Range(0.0f, 1.0f)]
    public float _timeBetweenSpawns;

    public void Awake()
    {
        _hexagons = new List<HexagonBehaviour>();
        _raycastDone = false;
        _currentTime = 0;
        _currentIndex = 0;
    }

    public override void Reset()
    {
        _raycastDone = false;
        _hexagons.Clear();
        _currentTime = 0;
        _currentIndex = 0;
    }

    private GameObject GetUnderHex(string hex)
    {
        string path = "prefabs/SM_Underhex_" + hex;

        return Resources.Load<GameObject>(path);
    }

    public void Update()
    {
        if (_play)
        {
            _currentTime += Time.deltaTime;

            if (!_raycastDone)
            {
                RaycastHit[] raycasts = Physics.RaycastAll(new Ray(_from, _to - _from), Vector3.Distance(_from, _to), LayerMask.GetMask("HexagonBigCollider"));
                Logger.Debug(raycasts.Length);
                for (int i = 0; i < raycasts.Length; i++)
                {
                    _hexagons.Add(raycasts[i].transform.GetComponentInParent<HexagonBehaviour>());
                }
                _raycastDone = true;
            }

            if (_currentIndex < _hexagons.Count && _currentTime > _timeBetweenSpawns)
            {
                _currentTime = 0;
                Logger.Debug(_hexagons[_currentIndex]._hexagonType);
                GameObject prefab = GetUnderHex(_hexagons[_currentIndex]._hexagonType);
                if (prefab != null)
                {
                    GameObject go = (GameObject)Instantiate(prefab, _hexagons[_currentIndex].transform.position, Quaternion.identity);
                    go.transform.Rotate(new Vector3(0, 0, 180));
                }
                else
                {
                    Logger.Debug("No prefab for underhex");
                }
                _currentIndex++;
            }
        }
    }
}
