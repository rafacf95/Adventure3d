using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDemageable
{
    public UiFillUpdater UiFillUpdater;
    public float startLife = 10f;
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [SerializeField] private float _currentLife;
    [SerializeField] private bool _destroyOnKill = true;

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UiUpdate();
    }

    protected virtual void Kill()
    {
        if (_destroyOnKill)
            Destroy(gameObject, 3);

        OnKill?.Invoke(this);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UiUpdate();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UiUpdate()
    {
        if (UiFillUpdater != null)
        {
            UiFillUpdater.UpdateValue((float)_currentLife / startLife);
        }
    }

#if UNITY_EDITOR

    [NaughtyAttributes.Button]
    public void TakeDamage()
    {
        Damage(5);
    }

#endif
}
