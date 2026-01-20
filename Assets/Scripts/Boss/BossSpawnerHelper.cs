using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;
using Cinemachine;

public class BossSpawnerHelper : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] string tagToCompare = "Player";
    [SerializeField] GameObject boss;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<Transform> waypoints;

    [Header("Camera Setup")]
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] GameObject bossCamera;

    private GameObject _currSpawned;

    private void AddNewTarget(Transform newTarget, float weigth = 1f, float radius = 0)
    {
        if (newTarget != null)
        {
            targetGroup.AddMember(newTarget, weigth, radius);
        }
    }

    private void RemoveTarget(Transform targetToRemove)
    {
        if (targetToRemove != null)
        {
            targetGroup.RemoveMember(targetToRemove);
        }
    }

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
                AddNewTarget(_currSpawned.transform);
                bossCamera.SetActive(true);
            }
        }
    }
}
