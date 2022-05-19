using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrajectoryRendererAdvanced : MonoBehaviour
{
    
    public GameObject BulletPrefab;
    [SerializeField]
    private LineRenderer _firstLine, _secondLine, _thirdLine;
    [SerializeField]
    private GameObject _arrowDirection;

    private BallsContainer _container;
    private bool _isMine;
    
    public void InitContrainer(BallsContainer _container, bool _isMine)
    {
        this._container = _container;
        this._isMine = _isMine;
    }
    
    public void ShowTrajectory(Vector3 origin, Vector3 direction, float strength)
    {
        GhostBullet _ghostBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity)
            .GetComponent<GhostBullet>();
        
        _ghostBullet.Init(this,direction * strength * 100);
    }

    public void ShowLine(List<Vector3> points)
    {
      //  _container.DeselectAll();
        
        points.Insert(0, transform.position);

        Vector3 dir = _arrowDirection.transform.position - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        //Debug.DrawRay(transform.position, dir * 1000, Color.green, 10);
        if (Physics.Raycast(transform.position, dir * 100, out RaycastHit hit, Mathf.Infinity))
        {
            if(points.Count == 1)
                points.Add(hit.point);
            if (hit.collider.TryGetComponent<Ball>(out Ball _ball))
            {
                _container.SelectBall(_ball, _isMine, _ball.Attack.Damage);
            }
        }
        
        if (points.Count < 2 && points.Count != 1)
        {
            _firstLine.positionCount = 2;
        
            _firstLine.SetPosition(0, points[0]);
            _firstLine.SetPosition(1, points[1]);
        }
        else if (points.Count > 2)
        {
            _firstLine.positionCount = 2;
        
            _firstLine.SetPosition(0, points[0]);
            _firstLine.SetPosition(1, points[1]);

            _secondLine.positionCount = 2;
        
            _secondLine.SetPosition(0, points[1]);
            _secondLine.SetPosition(1, points[1]);

            _thirdLine.positionCount = 2;
        
            _thirdLine.SetPosition(0, points[1]);
            _thirdLine.SetPosition(1, points[2]);
        }
        
        if (Physics.Raycast(transform.position, points[1] - transform.position, out RaycastHit _hit, Mathf.Infinity))
        {
            if (_hit.collider.TryGetComponent<Ball>(out Ball _ball))
            {
                _container.SelectBall(_ball, _isMine, _ball.Attack.Damage);
            }
        }
    }

    public void EnableLines()
    {
        _firstLine.gameObject.SetActive(true);
        _secondLine.gameObject.SetActive(true);
        _thirdLine.gameObject.SetActive(true);
    }

    public void DisableLines()
    {
        _firstLine.gameObject.SetActive(false);
        _secondLine.gameObject.SetActive(false);
        _thirdLine.gameObject.SetActive(false);
    }

}