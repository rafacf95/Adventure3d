using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{

    [Header("Setup")]
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Color color = Color.red;
    public float duration = .2f;

    private Color _defaultColor;
    private Tween _currTween;

    void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }
    void Start()
    {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Flash()
    {
        if (meshRenderer != null && !_currTween.IsActive())
        {
            _currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
        else if (skinnedMeshRenderer != null && !_currTween.IsActive())
        {
            _currTween = skinnedMeshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
