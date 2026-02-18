using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectables;

public class ItemCollectableLife : ItemCollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        // ItemManager.Instance.AddLife();
    }
}
