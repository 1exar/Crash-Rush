using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Entitys/Database")]
public class EntityDataBase : ScriptableObject
{
    [SerializeField] private List<EntityData> _playerEntitys = new List<EntityData>();
    [SerializeField] private List<EntityData> _enemyEntitys = new List<EntityData>();

    public EntityData GetPlayerEntityByType(EntityType type)
    {
        return _playerEntitys.Where(entity => entity.type == type).ToArray()[0];
    }
    
    public EntityData GetEnemyEntityByType(EntityType type)
    {
        return _enemyEntitys.Where(entity => entity.type == type).ToArray()[0];
    }
    
}

[Serializable]
public class EntityData
{

    public string name;
    public string description;
    public GameObject prefab;
    public EntityType type;
    public EntitySettings settings;
    
}
[Serializable]
public struct EntitySettings
{

    public int health;
    public float weight;
    public float drag;
    public float maxSpeed;
    public float maxDamage;
    public float minDamage;

}