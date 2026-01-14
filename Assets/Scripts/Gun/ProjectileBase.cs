using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public int damage = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void OnCollisionEnter(Collision collision)
    {

        foreach (var tag in tagsToHit)
        {
            if (collision.transform.CompareTag(tag))
            {
                var damageable = collision.transform.GetComponent<IDemageable>();

                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    // damageable.Damage(damage);
                    damageable.Damage(damage, dir);
                    Destroy(gameObject);
                }

                break;
            }
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
