using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    // private string _checkpointKey = "ChekpointKey";
    private bool _active = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!_active && other.transform.CompareTag("Player"))
        {
            VerifyCheckpoint();
        }
    }

    private void VerifyCheckpoint()
    {
        TurnOn();
        SaveCheckpoint();
    }

    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }

    private void SaveCheckpoint()
    {
        // if (PlayerPrefs.GetInt(_checkpointKey, 0) > key)
        //     PlayerPrefs.SetInt(_checkpointKey, key);

        CheckpointManager.Instance.SaveCheckpoint(key);
        SaveManager.Instance.SaveGame(1, key);
        _active = true;
    }
}
