using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public int damage = 1;
    public float speed = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.transform.GetComponent<IDemageable>();

        if (damageable != null)
        {
            damageable.Damage(damage);
            Destroy(gameObject);
        }

    }

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
