using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();
    }
    private void StartShoot()
    {
        gunBase.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void StopShoot()
    {
        gunBase.StopShoot();
        Debug.Log("Stop Shoot");
    }
}
