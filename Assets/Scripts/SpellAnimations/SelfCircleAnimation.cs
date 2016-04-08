using UnityEngine;
using System.Collections;

public class SelfCircleAnimation : SpellAnimation {

    public Animator _animator;
    public Renderer _haloMat;
    public Renderer _circleMat;

    private Color _baseColor;

    [Range(0, .5f)]
    public float _alphaHalo;
    [Range(0, .5f)]
    public float _alphaCircle;

    void Awake()
    {
        _baseColor = _haloMat.material.GetColor("_TintColor");
    }

    void Update()
    {
        if (_play)
        {
            Logger.Debug("Pop");
            gameObject.transform.position = _from;
            _animator.SetTrigger("play");

            _play = !_play;
        }

        _haloMat.material.SetColor("_TintColor", new Color(_baseColor.r, _baseColor.g, _baseColor.b, _alphaHalo));
        _circleMat.material.SetColor("_TintColor", new Color(_baseColor.r, _baseColor.g, _baseColor.b, _alphaCircle));
    }
}
