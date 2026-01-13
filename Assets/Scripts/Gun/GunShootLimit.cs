using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public float maxShoot = 5;
    public float timeToRecharge = 1f;

    private float _currentShots;
    private bool _recharging = false;

    protected override IEnumerator ShootCoroutine()
    {
        // if (_recharging) yield break;

        while (!_recharging)
        {
            if (_currentShots < maxShoot)
            {
                Shoot();
                _currentShots++;
                UpdateUI();
                CheckRecharge();
                yield return new WaitForSeconds(timeBetweenShoots);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShots));
    }
}
