using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Setup")]
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float speed = 50f;
    public float timeBetweenShoots = .3f;
    public List<UIGunUpdater> uIGunUpdaters;

    private Coroutine _currentCoroutine;

    protected virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.SetPositionAndRotation(positionToShoot.position, positionToShoot.rotation);
        projectile.speed = speed;
    }

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }
}
