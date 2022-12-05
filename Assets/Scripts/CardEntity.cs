using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private EntityType _currentTYpe;
    public bool canChoise = false;

    private void Start()
    {
        print(transform.position);
    }

    public void SetEntityType(EntityType type)
    {
        _currentTYpe = type;
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        //transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), .3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.DOScale(Vector3.one, .3f);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canChoise)
            EntitySpawner.InvokeChoiseBallEvent(_currentTYpe, true);
    }
}
