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
    [Range(0.0f, 1.0f)]
    public float _upwardSpeed;
    public float _timeStayingUp;

    public AnimationCurve _curve;

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
                for (int i = 0; i < raycasts.Length; i++)
                {
                    _hexagons.Add(raycasts[i].transform.GetComponentInParent<HexagonBehaviour>());
                }
                _raycastDone = true;
            }

            if (_currentIndex < _hexagons.Count && _currentTime > _timeBetweenSpawns)
            {
                _currentTime = 0;
                GameObject prefab = GetUnderHex(_hexagons[_currentIndex]._hexagonType);
                if (prefab != null)
                {
                    Vector3 position = _hexagons[_currentIndex].transform.position - new Vector3(0, 2.4f, 0);
                    GameObject go = (GameObject)Instantiate(prefab, position, Quaternion.identity);
                    go.transform.localScale = new Vector3(0.7f, 1, 0.7f);
                    go.layer = LayerMask.NameToLayer("EarthAnim");
                    go.transform.Rotate(new Vector3(0, 0, 180));
                    EarthBehaviour earthBehaviour = go.AddComponent<EarthBehaviour>();
                    go.AddComponent<SetRenderQueue>();
                    earthBehaviour.InitialPosition = position;
                    earthBehaviour.Speed = _upwardSpeed;
                    earthBehaviour.TimeStayingUp = _timeStayingUp;
                    earthBehaviour.MaxHeight = _curve.Evaluate((float)(_currentIndex + 1) / (float)_hexagons.Count) * 2.4f;
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
