using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using Events;
using UnityEngine;
using UnityEngine.Events;

public class ExpController : MonoBehaviour
{

    [SerializeField] private ExpData _playerData;
    [SerializeField] private ExpData _enemyData;
    
    private void Awake()
    {
        NewEventSystem.OnExpOrbPickup.Subscribe(PickUpOrb);
        NewEventSystem.OnPlayerLevelUp.Subscribe(OnPlayerLevelUp);
    }

    private void OnDisable()
    {
        NewEventSystem.OnExpOrbPickup.UnSubscribe(PickUpOrb);
        NewEventSystem.OnPlayerLevelUp.UnSubscribe(OnPlayerLevelUp);
    }

    private void PickUpOrb(bool isMine)
    {
        if (isMine)
        {
            _playerData.OnOrbPickup();
        }
        else
        {
            _enemyData.OnOrbPickup();
        }
    }

    private void OnPlayerLevelUp(bool isPlayer)
    {
        if(isPlayer)
            WindowController.ShowWindow(typeof(CardsControllerWindow));
    }
    
}

[Serializable]
public class ExpData
{
    public int currentLevel;
    public float currentXp;
    public float getPerOrb;
    public List<int> needToNextLevel = new List<int>();
    public bool isPlayer;
    
    public void OnOrbPickup()
    {
        currentXp += getPerOrb;
        if (currentXp >= needToNextLevel[0])
        {
            currentXp -= needToNextLevel[0];
            currentLevel++;
            NewEventSystem.OnPlayerLevelUp.InvokeEvent(isPlayer);
        }
    }
}
