using UnityEngine;
using DG.Tweening;

public class EntityCircleAnimation : MonoBehaviour
{
    private void Awake()
    {
        transform.DOScale(Vector3.one * 0.6f, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }
}
