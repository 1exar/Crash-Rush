using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsContainer : MonoBehaviour
{
    [SerializeField]
    private List<Ball> _myBalls = new List<Ball>();
    public List<Ball> MyBall
    {
        get { return _myBalls; }
        protected set {}
    }
    [SerializeField]
    private List<Ball> _enemyBalls = new List<Ball>();
    public List<Ball> EnemyBalls
    {
        get { return _enemyBalls; }
        protected set {}
    }

    public void AddMyBall(Ball ball)
    {
        _myBalls.Add(ball);
    }

    public void AddEnemyBall(Ball ball)
    {
        _enemyBalls.Add(ball);
    }

    public bool isMyBallsMoving()
    {
        foreach (var ball in _myBalls)
        {
            if (ball.isMoving())
            {
                return true;
            }
        }
        return false;
    }

    public bool isEnemyBallsMoving()
    {
        foreach (var ball in _enemyBalls)
        {
            if (ball.isMoving())
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveBall(Ball ball)
    {
        if (_myBalls.Contains(ball))
        {
            _myBalls.Remove(ball);
        }
        else if (_enemyBalls.Contains(ball))
        {
            _enemyBalls.Remove(ball);
        }
    }

    public Vector3 GetRandomBall(Vector3 _myPosition)
    {
        return _myBalls[Random.Range(0, _myBalls.Count)].transform.position;
    }

    public Vector3 FindClosestBall(Vector3 _myPosition)
    {
        Vector3 result = Vector3.zero;
        float distance = float.MaxValue;

        foreach (Vector3 pos in _myBalls.Select(b => b.transform.position))
        {
            if (Vector3.Distance(pos, _myPosition) < distance)
            {
                distance = Vector3.Distance(pos, _myPosition);
                result = pos;
            }
        }
        
        return result;
    }

    public void SelectBall(Ball _ball, bool _isMine, int _damage)
    {
        DeselectAll();
        if (!_isMine)
        {
            foreach (var ball in _myBalls)
            {
                if (_ball == ball)
                {
                    _ball.View.SelectBall();
                    _ball.Health.PreviewDamage(_damage);
                    break;
                }
            }
        }
        else
        {
            foreach (var ball in _enemyBalls)
            {
                if (_ball == ball)
                {
                    _ball.Health.PreviewDamage(_damage);
                    _ball.View.SelectBall();
                    break;
                }
            }
        }
    }

    public void DeselectAll()
    {
        foreach (var ball in _myBalls)
        {
            ball.View.DeselectBall();
            ball.Health.CancelPreview();
        }

        foreach (var ball in _enemyBalls)
        {
            ball.View.DeselectBall();
            ball.Health.CancelPreview();
        }
    }
    
}
