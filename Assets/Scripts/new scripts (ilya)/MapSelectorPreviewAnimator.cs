using UnityEngine;
using DG.Tweening;

public class MapSelectorPreviewAnimator : MonoBehaviour
{
    [Header("Show Map Preview Info Properties")]
    [SerializeField] private RectTransform mapPreviewTransform;
    [SerializeField] private RectTransform mapPreviewInfoTransform;
    [SerializeField] private float mapPreviewCoord;
    [SerializeField] private float mapPreviewInfoCoord;
    [SerializeField] private float showMapPreviewInfoDuration;

    [Header("Show Cards Properties")]
    [SerializeField] private RectTransform cardsPanelTransform;
    [SerializeField] private RectTransform mapSelectorTransform;
    [SerializeField] private float cardsPanelCoord;
    [SerializeField] private float mapSelectorCoord;
    [SerializeField] private float showCardsDuration;

    public void ShowMapPreviewInfo()
    {
        mapPreviewTransform.DOLocalMoveX(mapPreviewTransform.localPosition.x + mapPreviewCoord, showMapPreviewInfoDuration).SetEase(Ease.OutCubic);
        mapPreviewInfoTransform.DOLocalMoveX(mapPreviewInfoTransform.localPosition.x + mapPreviewInfoCoord, showMapPreviewInfoDuration).SetEase(Ease.OutCubic);
    }

    public void ShowCards()
    {
        cardsPanelTransform.DOLocalMoveY(cardsPanelTransform.localPosition.y + cardsPanelCoord, showCardsDuration).SetEase(Ease.OutCubic);
        mapSelectorTransform.DOLocalMoveY(mapSelectorTransform.localPosition.y + mapSelectorCoord, showCardsDuration).SetEase(Ease.OutCubic);
    }
}
