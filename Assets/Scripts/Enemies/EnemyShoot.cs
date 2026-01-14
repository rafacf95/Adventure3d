using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyShoot : EnemyBase
{
    public GunBase GunBase;

    [SerializeField] private string _compareTag = "Player";
    [SerializeField] private bool _lookAtPlayer = false;
    [SerializeField] private Vector3 _position;

    protected override void Init()
    {
        base.Init();

        // GunBase.StartShoot();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(_compareTag))
        {
            _position = other.transform.position;
            _lookAtPlayer = true;
            GunBase.StartShoot();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag(_compareTag))
            _position = other.transform.position;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(_compareTag))
        {
            _lookAtPlayer = false;
            GunBase.StopShoot();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_lookAtPlayer)
            transform.LookAt(_position);



    }

}
