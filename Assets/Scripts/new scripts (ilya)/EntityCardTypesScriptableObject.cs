using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityCardTypes", menuName = "Entitys/EntityCardTypes")]
public class EntityCardTypesScriptableObject : ScriptableObject
{
    public List<EntityCardType> EntityCardTypes;

    [Serializable]
    public struct EntityCardType
    {
        public EntityType EntityType;
        public Color EntityPreviewColor;
    }
}
