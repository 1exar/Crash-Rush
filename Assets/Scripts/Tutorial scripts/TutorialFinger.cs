using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialFinger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(1, 0.2f).OnComplete(() =>
            Invoke(nameof(SetTweening), 0));
    }

    private void SetTweening()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOMoveZ(-12, 1f).SetEase(Ease.OutSine));
        mySequence.Append(GetComponent<SpriteRenderer>().DOFade(0, 0.5f));
        mySequence.SetLoops(-1, LoopType.Restart);
    }
}
