using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    public GameObject uiChekpoint;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
            ActivateUi();
            Invoke(nameof(DeactivateUi), 2f);
        }
    }

    public void ActivateUi()
    {
        uiChekpoint.SetActive(true);
    }

    private void DeactivateUi()
    {
        uiChekpoint.SetActive(false);
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

}
