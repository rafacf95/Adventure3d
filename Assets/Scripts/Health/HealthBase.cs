using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [SerializeField] private float _currentLife;

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
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
        OnDamage?.Invoke(this);
    }

#if UNITY_EDITOR

    [NaughtyAttributes.Button]
    public void TakeDamage()
    {
        Damage(5);
    }

#endif
}
