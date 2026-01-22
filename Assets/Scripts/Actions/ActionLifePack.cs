using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectables;

public class ActionLifePack : MonoBehaviour
{

    public SOInt soInt;
    public KeyCode keyCode = KeyCode.L;

    void Start()
    {
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife()
    {
        if (soInt.value > 0)
        {
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
        }
    }

    void Update()
    {
        if (Input.GetKey(keyCode))
        {
            RecoverLife();
        }
    }

}
