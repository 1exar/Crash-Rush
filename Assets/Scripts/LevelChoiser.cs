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
                _spawner.PrepareSpawn(_levels[0]);
                break;
            case Level.farm:
                _levels[1].gameObject.SetActive(true);
                _spawner.PrepareSpawn(_levels[1]);
                break;
            case Level.egypt:
                _levels[2].gameObject.SetActive(true);
                _spawner.PrepareSpawn(_levels[2]);
                break;
        }
    }

    private void ChoiseLevel(LevelSettings level)
    {
        level.gameObject.SetActive(true);
        _spawner.SpawnEntity(level.mySpawnPos,level.enemySpawnPos);
    }
    
}
