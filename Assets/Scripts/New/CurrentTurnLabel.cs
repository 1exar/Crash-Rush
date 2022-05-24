using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentTurnLabel : MonoBehaviour
{
    [SerializeField] private Transform textTransform;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float duration = 0.5f;

    public void SetText(string newText)
    {
        text.text = newText;
    }

    public void Show()
    {
        StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        text.DOFade(1f, duration / 2);

        textTransform.localScale = Vector3.one / 2;
        textTransform.DOScale(1, duration);

        yield return new WaitForSeconds(1f);

        text.DOFade(0f, duration / 2);
    }
}
