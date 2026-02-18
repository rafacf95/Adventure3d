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
    public List<UiFillUpdater> uiFillUpdaters;

    [Header("Audio Setup")]
    public SFXType sfxType;

    private Coroutine _currentCoroutine;


    private void PlaySFX(SFXType sfxType)
    {
        SFXPool.Instance.Play(sfxType);
    }

    protected virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.SetPositionAndRotation(positionToShoot.position, positionToShoot.rotation);
        projectile.speed = speed;

        PlaySFX(sfxType);
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
