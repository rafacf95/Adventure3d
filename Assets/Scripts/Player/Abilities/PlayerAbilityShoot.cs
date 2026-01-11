using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public List<UIGunUpdater> uIGunUpdaters;
    public List<GunBase> gunBases;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();
        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => StopShoot();

        if (gunBases.Count > 1)
        {
            inputs.Gameplay.ChangeGun1.performed += ctx => ChangeGun(gunBases[0]);
            inputs.Gameplay.ChangeGun2.performed += ctx => ChangeGun(gunBases[1]);
        }
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase, gunPosition);
        _currentGun.uIGunUpdaters = uIGunUpdaters;
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void ChangeGun(GunBase gunType = null)
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }
        _currentGun = Instantiate(gunType, gunPosition);
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
