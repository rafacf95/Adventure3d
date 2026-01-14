using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class BossSpawnerHelper : MonoBehaviour
{
    [SerializeField] string tagToCompare = "Player";
    [SerializeField] GameObject boss;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<Transform> waypoints;

    private GameObject _currSpawned;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrei");
        if (_currSpawned == null)
        {
            if (other.transform.CompareTag(tagToCompare))
            {
                _currSpawned = Instantiate(boss);
                _currSpawned.transform.position = spawnPoint.transform.position;
                var bossBase = _currSpawned.GetComponent<BossBase>();
                bossBase.waypoints = waypoints;
            }
        }
    }
}
