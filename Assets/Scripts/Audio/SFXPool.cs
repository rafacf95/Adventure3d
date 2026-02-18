using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;
    private List<AudioSource> _audioSourceList;
    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }

    }

    private void CreateAudioSourceItem()
    {
        GameObject sfxPool = new GameObject("SFXpool");
        sfxPool.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(sfxPool.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if (_index >= _audioSourceList.Count)
        {
            _index = 0;
        }
    }
}
