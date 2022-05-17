using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private TrajectoryRendererAdvanced _trajectory;
    [SerializeField]
    private TrailRenderer _trailRenderer;

    public TrailRenderer TrailRenderer
    {
        get { return _trailRenderer; }
        protected set {}
    }
    
    private bool _savePoints = false;

    private List<Vector3> _collisionPoints = new List<Vector3>();

    private IEnumerator Start()
    {
        _savePoints = true;
        yield return new WaitForSeconds(.2f);
        Vector3[] _points = new Vector3[_trailRenderer.positionCount];
        _trailRenderer.GetPositions(_points);
        _trajectory.ShowLine(_collisionPoints);
        Destroy(gameObject);
    }

    public void Init(TrajectoryRendererAdvanced _trajectory, Vector3 _direction)
    {
        this._trajectory = _trajectory;
        _rigidbody.AddForce(_direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionPoints.Add(collision.GetContact(0).point);
    }
}
