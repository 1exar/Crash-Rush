using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entitys/Database")]
public class EntityDataBase : ScriptableObject
{
    [SerializeField] private List<EntityData> entitys = new List<EntityData>();

    public List<EntityData> Entitys
    {
        get { return entitys; }
        protected set { }
    }
    
}

[Serializable]
public class EntityData
{

    public string name;
    public GameObject prefab;
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