using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{

    [Header("Setup")]
    public MeshRenderer meshRenderer;
    public Color color = Color.red;
    public float duration = .2f;

    private Color _defaultColor;
    private Tween _currTween;

    void Start()
    {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Flash()
    {
        if (!_currTween.IsActive())
            _currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
