using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : EnemyBase
{
    [Header("Walking setup")]
    public GameObject[] waypoints;
    public float minDistance = 1f;
    public float speed = 15f;
    
    private int _index = 0;



    protected override void Update()
    {
        base.Update();

        Walk();
    }

    protected void Walk()
    {
        if(Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
        {
            _index ++;
            if(_index >= waypoints.Length)
            {
                _index = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, speed *Time.deltaTime);
        transform.LookAt(waypoints[_index].transform.position);
    }

}
