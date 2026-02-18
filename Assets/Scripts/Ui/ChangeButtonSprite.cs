using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;

    [SerializeField]
    private Image _buttonImage;

    [SerializeField]
    private bool _isMuted = false;

    void OnValidate()
    {
        _buttonImage = GetComponent<Image>();

    }
    void Start()
    {
        OnValidate();
    }

    public void SwapImage()
    {
        _isMuted = !_isMuted;
        if (!_isMuted)
        {
            _buttonImage.sprite = spriteOn;
        }
        else
        {
            _buttonImage.sprite = spriteOff;
        }
    }


}
