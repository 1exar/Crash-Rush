﻿using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using DG.Tweening;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardEntity : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private EntityType _currentTYpe;
    public bool canChoise = false;

    public RawImage image;
    public TMP_Text _text, _name;
    public EntityDataBase _data;

    public bool forAds;
    
    public void SetEntityType(EntityType type, RenderTexture img)
    {
        image.texture = img;
        _currentTYpe = type;
        _text.text = _data.GetPlayerEntityByType(type).description;
        _name.text = _data.GetPlayerEntityByType(type).name;
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
        if (canChoise)
        {
            NewEventSystem.OnChooseNewBallEvent.InvokeEvent(_currentTYpe,true);
            WindowController.CloseWindow(typeof(CardsControllerWindow));
        }
    }
}
