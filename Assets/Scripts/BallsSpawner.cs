using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
    [SerializeField]
    private BallsContainer _balls;
    [SerializeField]
    private GameObject _ballPrefab;
    [SerializeField]
    private Transform[] _mySpawnPoins;
    [SerializeField]
    private Transform[] _enemySpawnPoints;

    private void Start()
    {
        SpawnBalls(true);
        SpawnBalls(false);
    }

    private void SpawnBalls(bool _isMine)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_isMine)
            {
                var ball = Instantiate(_ballPrefab, _mySpawnPoins[i].position, Quaternion.identity).GetComponent<Ball>();
                ball.Init(true, _balls);
                _balls.AddMyBall(ball);
            }
            else
            {
                var ball = Instantiate(_ballPrefab, _enemySpawnPoints[i].position, Quaternion.identity).GetComponent<Ball>();
                ball.Init(false, _balls);
                _balls.AddEnemyBall(ball);
            }
        }
        
    }

}
