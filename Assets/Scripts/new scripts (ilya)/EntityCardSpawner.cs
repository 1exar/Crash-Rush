using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntityCardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<CardsContainerData> cardsContainersData;
    [SerializeField] private EntityCardTypesScriptableObject entityCardTypesData;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (CardsContainerData cardParent in cardsContainersData)
        {
            for (int i = 0; i < cardParent.CardAmount; i++)
            {
                CardEntity newCard = Instantiate(cardPrefab, cardParent.CardParentTransform).GetComponent<CardEntity>();
                SetRandomCardType(newCard);
            }
        }
    }

    private void SetRandomCardType(CardEntity card)
    {
        int entityTypeIndex = Random.Range(1, 6);

        EntityType type = entityCardTypesData.EntityCardTypes[entityTypeIndex].EntityType;
        Color entityPreviewColor = entityCardTypesData.EntityCardTypes[entityTypeIndex].EntityPreviewColor;

        card.SetEntityType(type, entityPreviewColor);
    }

    [Serializable]
    public struct CardsContainerData
    {
        public Transform CardParentTransform;
        public int CardAmount;
    }
}
