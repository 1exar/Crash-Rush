using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.localScale /= 2;
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutSine);
    }
}
