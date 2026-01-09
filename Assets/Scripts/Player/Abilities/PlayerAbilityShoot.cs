using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public List<UIGunUpdater> uIGunUpdaters;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();
        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.uIGunUpdaters = uIGunUpdaters;
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void StopShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Stop Shoot");
    }
}
