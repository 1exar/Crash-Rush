using UnityEngine;
using DG.Tweening;
using System.Collections;

public class StarAnimation : MonoBehaviour
{
    private void Awake()
    {
        transform.DORotate(Vector3.up * 180, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        transform.DOMoveY(transform.position.y + 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        StartCoroutine(DeleteByTime());
    }

    private IEnumerator DeleteByTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
