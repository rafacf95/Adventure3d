using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour, IDemageable
{
    [Header("Enemy Setup")]
    public float startLife = 10f;
    public Collider col;
    public FlashColor flashColor;
    [SerializeField] private float _currentLife;
    [SerializeField] UIEnemyUpdater _uIEnemyUpdater;

    [Header("Start Animation")]
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;

    [Header("Animation")]
    [SerializeField] protected AnimationBase _animationBase;

    private void Awake()
    {
        Init();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Init()
    {
        ResetLife();
        UpdateUi();
    }

    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill()
    {
        if (col != null) col.enabled = false;
        Destroy(gameObject, 3);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float damage)
    {
        _currentLife -= damage;
        UpdateUi();

        if (flashColor != null)
            flashColor.Flash();

        if (_currentLife <= 0)
        {
            Kill();
        }
    }

    protected void UpdateUi()
    {
        if (_uIEnemyUpdater != null)
            _uIEnemyUpdater.UpdateValue(startLife, _currentLife);
    }

    protected void SpawnAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationTrigger(animationType);
    }

    public void Damage(float damage)
    {
        OnDamage(damage);
    }
}
