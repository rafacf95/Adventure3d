using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    public float duration = .1f;
    [SerializeField] Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {

        Vignette temp;

        if (processVolume.profile.TryGetSettings<Vignette>(out temp))
        {
            _vignette = temp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }
    }
}
