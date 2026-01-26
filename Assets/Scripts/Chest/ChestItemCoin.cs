using System.Collections;
using System.Collections.Generic;
using Collectables;
using DG.Tweening;
using UnityEngine;

public class ChestItemCoin : ChestItemBase
{

    [Header("Setup")]
    public int coinNumber = 10;
    public GameObject coinObject;
    public Vector2 randomRange = new Vector2(-2f, 2f);

    [Header("Animation Setup")]
    public float tweenDuration = .2f;
    public Ease ease = Ease.OutBack;

    private List<GameObject> _items = new List<GameObject>();

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItens();
    }

    [NaughtyAttributes.Button]
    private void CreateItens()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);

            // item.transform.position = transform.position;
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);

            item.transform.DOScale(0, tweenDuration).SetEase(ease).From();

            _items.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach (var i in _items)
        {
            i.transform.DOMoveY(2f, tweenDuration).SetRelative();
            i.transform.DOScale(0, tweenDuration / 2).SetDelay(tweenDuration / 2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
