using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntityCardsSpinner : MonoBehaviour
{
    [SerializeField] private float cardHeight = 450f;
    [SerializeField] private float cardSpinSpeed = 0.1f;
    [SerializeField] private float slowDownDuration = 2f;

    [Header("Final card animation")]
    [SerializeField] private float scaleAnimationValue = 1.25f;
    [SerializeField] private float scaleAnimationDuration = 0.5f;
    [Header("")]

    [SerializeField] private List<CardsSpinnersData> cardsSpinnersData = new List<CardsSpinnersData>();

    public void Spin()
    {
        foreach (CardsSpinnersData spinner in cardsSpinnersData)
        {
            int cardsAmount = spinner.CardsAmount - 1;

            float spinDuration = cardSpinSpeed * cardsAmount;

            float continuousSpinDuration = spinDuration - slowDownDuration;
            int continuousSpinCardsAmount = Convert.ToInt32(continuousSpinDuration / cardSpinSpeed);

            int remainingCardsAmount = cardsAmount - continuousSpinCardsAmount;
            float remainingSpinDuration = cardSpinSpeed * remainingCardsAmount;

            float continuousSpinPathLength = cardHeight * continuousSpinCardsAmount;
            float remainingSpinPathLength = cardHeight * remainingCardsAmount;

            spinner.RectTransform.DOLocalMoveY(spinner.RectTransform.localPosition.y - continuousSpinPathLength, continuousSpinDuration).SetEase(Ease.InQuad).OnComplete(() =>
            {
                spinner.RectTransform.DOLocalMoveY(spinner.RectTransform.localPosition.y - remainingSpinPathLength, remainingSpinDuration).OnComplete(() =>
                {
                    ShowSpinResult(spinner);
                });
            });
        }
    }

    private void ShowSpinResult(CardsSpinnersData spinner)
    {
        Transform finalCard = spinner.RectTransform.GetChild(spinner.CardsAmount - 1);
        finalCard.SetParent(spinner.RectTransform.parent.parent); //get over the mask
        Destroy(spinner.RectTransform.gameObject);

        finalCard.DOScale(scaleAnimationValue, scaleAnimationDuration).SetLoops(2, LoopType.Yoyo);

        finalCard.gameObject.GetComponent<CardEntity>().canChoise = true;
    }
}

[Serializable]
public struct CardsSpinnersData
{
    public RectTransform RectTransform;
    public int CardsAmount;
}
