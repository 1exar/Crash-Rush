using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    [SerializeField] private RawImage _rawImage;

    private EntityType _currentTYpe;

    public void SetEntityType(EntityType type)
    {
        _currentTYpe = type;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), .3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, .3f);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EntitySpawner.InvokeChoiseBallEvent(_currentTYpe, true);
    }
}
