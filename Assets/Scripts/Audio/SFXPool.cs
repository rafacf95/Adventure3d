using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using UnityEngine.Audio;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;

    [SerializeField]
    AudioMixerGroup sfxMixerGroup;
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
        var audioSource = sfxPool.AddComponent<AudioSource>();
        audioSource.volume = .5f;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        _audioSourceList.Add(audioSource);
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;

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
