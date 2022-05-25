using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChoiser : MonoBehaviour
{
    [SerializeField]
    private List<LevelSettings> _levels = new List<LevelSettings>();
    [SerializeField]
    private EntitySpawner _spawner;

    private enum Level
    {
        forest, farm, egypt
    }
    [SerializeField]
    private bool _testMode = false;
    
    [SerializeField]
    private Level _currentLevel;

    private void Start()
    {
        if (_testMode == true)
        {
            ChoiseLevel(_currentLevel);
        }
        else
        {
            int level = PlayerPrefs.GetInt("level", 0);
            ChoiseLevel(_levels[level]);
        }
    }

    private void ChoiseLevel(Level level)
    {
        switch (level)
        {
            case Level.forest:
                _levels[0].gameObject.SetActive(true);
                _spawner.SpawnEntity(_levels[0].mySpawnPos, _levels[0].enemySpawnPos);
                break;
            case Level.farm:
                _levels[1].gameObject.SetActive(true);
                _spawner.SpawnEntity(_levels[1].mySpawnPos, _levels[1].enemySpawnPos);
                break;
            case Level.egypt:
                _levels[2].gameObject.SetActive(true);
                _spawner.SpawnEntity(_levels[2].mySpawnPos, _levels[2].enemySpawnPos);
                break;
        }
    }

    private void ChoiseLevel(LevelSettings level)
    {
        level.gameObject.SetActive(true);
        _spawner.SpawnEntity(level.mySpawnPos,level.enemySpawnPos);
    }
    
}
