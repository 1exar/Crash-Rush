using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnChoiser : MonoBehaviour
{
    [SerializeField]
    private BallsContainer _balls;
    [SerializeField]
    private UINotification _uiNotification;
    [SerializeField]
    private int _lastMyBall = 0;
    [SerializeField]
    private int _lastEnemyBall = 0;

    private bool _choiseNewTurn;
    private bool _lastTurn;
    
    private void Start()
    {
        foreach (var ball in _balls.MyBall)
        {
            ball.Movment.OnMoved += AfterTurn;
        }
        
        foreach (var ball in _balls.EnemyBalls)
        {
            ball.Movment.OnMoved += AfterTurn;
        }
        
       // int rnd = Random.Range(0, 2);
       int rnd = 0;
        if (rnd == 0)
        {
            ChoiseNextTurn(true);
        }
        else
        {
            ChoiseNextTurn(false);
        }
    }

    private void ChoiseNextTurn(bool isMine)
    {
        _uiNotification.ShowTurn(isMine);
        if (isMine)
        {

            if (_lastMyBall >= _balls.MyBall.Count)
            {
                _lastMyBall = 0;
            }
            
            _balls.MyBall[_lastMyBall].MakeTurn();
            _lastMyBall++;
            if (_lastMyBall >= _balls.MyBall.Count)
            {
                _lastMyBall = 0;
            }

            foreach (var ball in _balls.MyBall)
            {
                ball.MyTeamAttack();
            }
            
            foreach (var ball in _balls.EnemyBalls)
            {
                ball.EnemyTeamAttack();
            }
            
        }
        else
        {
            
            if (_lastEnemyBall >= _balls.EnemyBalls.Count)
            {
                _lastEnemyBall = 0;
            }
            
            _balls.EnemyBalls[_lastEnemyBall].MakeTurn();
            _lastEnemyBall++;
            if (_lastEnemyBall >= _balls.EnemyBalls.Count)
            {
                _lastEnemyBall = 0;
            }
            
            foreach (var ball in _balls.MyBall)
            {
                ball.EnemyTeamAttack();
            }
            
            foreach (var ball in _balls.EnemyBalls)
            {
                ball.MyTeamAttack();
            }
            
        }
    }   

    private void AfterTurn(bool _isMine)
    {
        _lastTurn = _isMine;
        _choiseNewTurn = true;
    }

    private void LateUpdate()
    {
        if(_choiseNewTurn == false) return;
        if (_balls.isEnemyBallsMoving() == false && _balls.isMyBallsMoving() == false)
        {
            _choiseNewTurn = false;
            StartCoroutine(WaiteBeforeNetTurn(_lastTurn));
        }
    }

    private IEnumerator WaiteBeforeNetTurn(bool _isMine)
    {
        yield return new WaitForSeconds(.7f);
        ChoiseNextTurn(!_isMine);
    }
    
}
