using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BallMovment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private bool _isMouseDowned;
    private Vector3 _mouseDownPos;
    private float _strength;
    [SerializeField]
    private GameObject _powerIndicator;
    [SerializeField]
    private GameObject _arrowDirection;
    private float _ortSizeStart;
    [SerializeField]
    private float _ortSizeMax;
    private bool _canMove;

    private bool _isMine;

    private BallsContainer _container;

    private Vector3 _direction;
    
    public event UnityAction<bool> OnMoved;
    [SerializeField] 
    private TrajectoryRendererAdvanced _trajectory;
    
    public void Init(bool isMine, BallsContainer _container)
    {
        _isMine = isMine;
        this._container = _container;
        _trajectory.InitContrainer(_container, isMine);
    }

    public bool isMoving()
    {
        if(_rigidbody.velocity == Vector3.zero && _rigidbody.angularVelocity == Vector3.zero && _rigidbody.IsSleeping())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    private void Awake()
    {
        _ortSizeStart = Camera.main.fieldOfView;
    }

    private void Update()
    {

        //  transform.rotation = Quaternion.Euler(Vector3.zero);

        if (_isMouseDowned)
        {

            CalculateTrajectory();

            if (Input.GetMouseButtonUp(0))
            {
                _rigidbody.AddForce(_direction * _strength * 5);

                _powerIndicator.transform.localScale = Vector3.one;
                _powerIndicator.transform.localPosition = Vector3.zero;

                _powerIndicator.gameObject.SetActive(false);
                _arrowDirection.gameObject.SetActive(false);

                _isMouseDowned = false;
                _canMove = false;

                OnMoved?.Invoke(_isMine);

                _trajectory.DisableLines();

            }
        }
    }

    private void CalculateTrajectory()
    {
        
        _direction = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            _strength = Vector3.Distance(transform.position, hit.point * 5);

            _direction = _mouseDownPos - hit.point;

            _direction = new Vector3(_direction.x, 0, _direction.z);

            transform.rotation = Quaternion.LookRotation(_direction);

            _powerIndicator.transform.localScale =
                new Vector3(_powerIndicator.transform.localScale.x, _strength / 20,
                    _powerIndicator.transform.localScale.z);

            _powerIndicator.transform.localPosition = new Vector3(_powerIndicator.transform.localPosition.x,
                _powerIndicator.transform.localPosition.y, _powerIndicator.transform.localScale.y / 2);

        }

        _trajectory.ShowTrajectory(transform.position, _direction, _strength);
    }
    
    private void OnMouseDown()
    {
        if(_canMove == false) return;
        _isMouseDowned = true;
        //_mouseDownPos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
            _mouseDownPos = hit.point;
        }
        
        //Camera.main.DOFieldOfView(_ortSizeMax, 1f);
        
        _trajectory.EnableLines();

        //  _powerIndicator.gameObject.SetActive(true);
      //  _arrowDirection.gameObject.SetActive(true);

    }

    public void MyTurn()
    {
        if (_isMine == true)
        {
            _canMove = true;
        }
        else
        {
            StartCoroutine(ProccesMyTurn());
        }
    }

    private IEnumerator ProccesMyTurn()
    {
        _powerIndicator.gameObject.SetActive(true);
        _arrowDirection.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(.3f);

        Vector3 _enemyPos = _container.FindClosestBall(transform.position);

        Vector3 _direction = Vector3.zero;

        _strength = Vector3.Distance(transform.position, _enemyPos * 7);

        _direction = transform.position - _enemyPos;

        _direction = new Vector3(_direction.x, 0, _direction.z);

        transform.DOLookAt(_enemyPos, 1.5f);
        
        yield return new WaitForSeconds(1.5f);
        
        _powerIndicator.transform.DOScale(new Vector3(_powerIndicator.transform.localScale.x, _strength / 20,
            _powerIndicator.transform.localScale.z), .5f);
        
        _powerIndicator.transform.DOLocalMove(new Vector3(_powerIndicator.transform.localPosition.x,
            _powerIndicator.transform.localPosition.y, _powerIndicator.transform.localScale.y / 2), .5f);
        yield return new WaitForSeconds(.5f);

        /*_powerIndicator.transform.localPosition = new Vector3(_powerIndicator.transform.localPosition.x,
            _powerIndicator.transform.localPosition.y, _powerIndicator.transform.localScale.y / 2);*/
        
        _rigidbody.AddForce(-_direction * _strength * 4); 
                
        _powerIndicator.transform.localScale = Vector3.one;
        _powerIndicator.transform.localPosition = Vector3.zero;

        _powerIndicator.gameObject.SetActive(false);
        _arrowDirection.gameObject.SetActive(false);
                
        _isMouseDowned = false;
        _canMove = false;

        yield return new WaitForSeconds(3.5f);
        
        OnMoved?.Invoke(_isMine);
    }
}