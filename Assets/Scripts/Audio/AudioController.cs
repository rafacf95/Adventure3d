using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioMixer _audioMixer;
    private const string MASTER_VOLUME_PARAM = "MasterVolume";

    [SerializeField]
    private bool _isMuted = false;

    [NaughtyAttributes.Button]
    public void ToggleSound()
    {
        _isMuted = !_isMuted;
        if (!_isMuted)
        {
            _audioMixer.SetFloat(MASTER_VOLUME_PARAM, 0f);
        }
        else
        {
            _audioMixer.SetFloat(MASTER_VOLUME_PARAM, -80f);
        }
    }
}
