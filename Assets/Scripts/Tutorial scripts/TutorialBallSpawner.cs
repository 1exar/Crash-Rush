using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBallSpawner : MonoBehaviour
{
    [SerializeField]
    private BallsContainer _balls;
    [SerializeField]
    private GameObject _myBallPrefab;
    [SerializeField]
    private GameObject _enemyBallPrefab;
    [SerializeField]
    private Transform[] _mySpawnPoints;
    [SerializeField]
    private Transform[] _enemySpawnPoints;

    private void Start()
    {
        SpawnBalls();
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < 2; i++)
        {
            var ball = Instantiate(_myBallPrefab, _mySpawnPoints[i].position, Quaternion.identity).GetComponent<Ball>();
            ball.Init(true, _balls);
            _balls.AddMyBall(ball);
            
            ball = Instantiate(_enemyBallPrefab, _enemySpawnPoints[i].position, Quaternion.identity).GetComponent<Ball>();
            ball.Init(false, _balls);
            _balls.AddEnemyBall(ball);
        }
    }
}
