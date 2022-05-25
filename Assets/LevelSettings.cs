using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private List<Transform> _mySpawnPos = new List<Transform>();
    [SerializeField]
    private List<Transform> _enemySpawnPos = new List<Transform>();

    public List<Transform> mySpawnPos
    {
        get { return _mySpawnPos; }
        protected set{}
    }

    public List<Transform> enemySpawnPos
    {
        get { return _enemySpawnPos; }
        protected set {}
    }
}
