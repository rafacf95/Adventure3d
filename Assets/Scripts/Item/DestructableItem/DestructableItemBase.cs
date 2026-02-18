using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(HealthBase))]
public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = .2f;
    public int shakeForce = 2;

    public int dropedCoinsAmount = 1;
    public GameObject coinPrefab;
    public Transform dropPosition;

    public float tweenDuration = .5f;
    public Ease ease = Ease.OutBack;

    void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    void Awake()
    {
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up / 2, shakeForce);
        DropCoins();
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, tweenDuration).SetEase(ease).From();
    }

    [NaughtyAttributes.Button]
    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropedCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
