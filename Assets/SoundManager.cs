using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static bool isOnSound = true;

    [SerializeField] private Sprite _onSprite, _offSprite;
    [SerializeField] private Image _soundImage;

    public void ChangeSound()
    {
        isOnSound = !isOnSound;
        NewEventSystem.onChangeSoundEvent.InvokeEvent(isOnSound);
        ChangeIcon();
    }

    private void ChangeIcon()
    {
        if (isOnSound)
        {
            _soundImage.sprite = _onSprite;
        }
        else
        {
            _soundImage.sprite = _offSprite;
        }
    }

}
