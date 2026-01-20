using UnityEngine;
using Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{

    public CinemachineVirtualCamera virtualCamera;
    public float shakeTime;

    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = .2f;

    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        var c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            Shake(0, 0, 0);
        }
    }
}
