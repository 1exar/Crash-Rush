using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpController : MonoBehaviour
{

    [SerializeField] private ExpData _playerData;
    [SerializeField] private ExpData _enemyData;

    private static UnityAction<bool> OnPickup;

    private void Awake()
    {
        OnPickup += PickUpOrb;
    }

    private void OnDisable()
    {
        OnPickup -= PickUpOrb;
    }

    private void PickUpOrb(bool isMine)
    {
        if (isMine)
        {
            _playerData.OnOrbPickup(() => GameUI.Instance.ShowNewCardPanel());
        }
        else
        {
          //  _enemyData.OnOrbPickup();
        }
    }

    public static void CallPickUpEvent(bool isMine)
    {
        OnPickup?.Invoke(isMine);
    }
    
}

[Serializable]
public class ExpData
{
    public int currentLevel;
    public float currentXp;
    public float getPerOrb;
    public List<int> needToNextLevel = new List<int>();

    public void OnOrbPickup(UnityAction OnLevelUp)
    {
        currentXp += getPerOrb;
        if (currentXp >= needToNextLevel[currentLevel])
        {
            currentXp -= needToNextLevel[currentLevel];
            currentLevel++;
            OnLevelUp?.Invoke();
        }
    }
}
