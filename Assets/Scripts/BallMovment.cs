using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BallMovment : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _strength;
    [SerializeField]
    private GameObject _powerIndicator;
    [SerializeField]
    private GameObject _arrowDirection;
    [SerializeField]
    private Transform _ballCanvas;
    [SerializeField]
    private float _ortSizeMax;

    private BallsContainer _container;
    private Vector3 _mouseDownPos;
    private Vector3 _direction;
    private Vector3 _lastVelocity;
    private float _ortSizeStart;
    private bool _isMouseDowned;
    private bool _canMove;
    private bool _isMine;

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
        _lastVelocity = GetComponent<Rigidbody>().velocity;
        //  transform.rotation = Quaternion.Euler(Vector3.zero);
        //_ballCanvas.rotation = Quaternion.Euler(new Vector3();

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

    private Vector3[] CalculateEnemyAttackTrajectory()
    {
        transform.Rotate(Vector3.up * Random.Range(0, 360));

        int reflectionsCount = 2;
        int multiplier = 1;
        if (Random.Range(0, 2) == 0) multiplier = -1;

        Vector3[] longestPath = new Vector3[reflectionsCount];
        float longestPathLong = 0;

        for (int i = 360; i > 0; i -= 5)
        {
            Vector3[] path = new Vector3[reflectionsCount];
            float pathLong = 0;

            Vector3 originPoint = transform.position;
            Vector3 direction = transform.forward;

            for (int j = 0; j < reflectionsCount; j++)
            {
                Ray ray = new Ray(originPoint, direction);

                if (Physics.SphereCast(ray, 1.5f, out RaycastHit hit))
                {
                    path[j] = hit.point;

                    Debug.DrawLine(originPoint, hit.point, Color.black, 20f);
                    if (hit.collider.TryGetComponent(out Ball ball))
                    {
                        if (ball.IsMine) return path;
                        else break;
                    }
                    else
                    {
                        pathLong += Vector3.Distance(originPoint, hit.point);

                        direction = Vector3.Reflect(direction, hit.normal);
                        direction.y = 0;

                        originPoint = hit.point;
                    }
                }
            }

            if (pathLong > longestPathLong) longestPath = path;

            transform.Rotate(Vector3.up * (5f * multiplier));
        }

        return longestPath;
    }
    private IEnumerator ProccesMyTurn()
    {
        _arrowDirection.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(.3f);

        Vector3 targetPoint = Vector3.zero;
        foreach (Ball ball in _container.MyBall)
        {
            targetPoint = ball.transform.position;

            if (Vector3.Distance(transform.position, targetPoint) < 3f) break;

            Ray ray = new Ray(transform.position, targetPoint - transform.position);
            if (Physics.SphereCast(ray, 1.5f, out RaycastHit hit))
            {
                Debug.Log(hit.collider.name);
                if (!hit.collider.TryGetComponent<Ball>(out Ball detectedBall))
                {
                    targetPoint = Vector3.zero;
                    continue;
                }
                else if (detectedBall.IsMine)
                {
                    targetPoint = ball.transform.position;
                    break;
                }
            }
        }

        if (targetPoint != Vector3.zero)
        {
            transform.DOLookAt(targetPoint, .5f);
            _strength = Vector3.Distance(transform.position, targetPoint) * 40;
        }
        else 
        {
            Vector3[] path = CalculateEnemyAttackTrajectory();

            float d1 = Vector3.Distance(transform.position, path[0]);
            _strength += d1 * 100;
            for (int i = 0; i < (path.Length - 1); i++)
            {
                _strength += Vector3.Distance(path[i], path[i + 1]) * 40;
            }

            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.DOLookAt(path[0], 0.5f);
        }


        yield return new WaitForSeconds(1.5f);
        
        _powerIndicator.transform.DOScale(new Vector3(_powerIndicator.transform.localScale.x, _strength / 20,
            _powerIndicator.transform.localScale.z), .5f);
        _powerIndicator.transform.DOLocalMove(new Vector3(_powerIndicator.transform.localPosition.x,
            _powerIndicator.transform.localPosition.y, _powerIndicator.transform.localScale.y / 2), .5f);

        yield return new WaitForSeconds(.5f);

        /*_powerIndicator.transform.localPosition = new Vector3(_powerIndicator.transform.localPosition.x,
            _powerIndicator.transform.localPosition.y, _powerIndicator.transform.localScale.y / 2);*/
        
        _rigidbody.AddForce(transform.forward * _strength * 4); 
                
        _powerIndicator.transform.localScale = Vector3.one;
        _powerIndicator.transform.localPosition = Vector3.zero;

        _powerIndicator.gameObject.SetActive(false);
        _arrowDirection.gameObject.SetActive(false);
                
        _isMouseDowned = false;
        _canMove = false;

        yield return new WaitForSeconds(3.5f);
        
        OnMoved?.Invoke(_isMine);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_lastVelocity == Vector3.zero) return;

            ContactPoint contact = collision.GetContact(0);
            Vector3 newDirection = Vector3.Reflect(_lastVelocity.normalized, contact.normal);
            float magnitude = _rigidbody.velocity.magnitude;
            _rigidbody.velocity = newDirection * magnitude;
        }

        else if (obj.GetComponent<Ball>())
        {
            _rigidbody.velocity /= 2f;
        }
    }
}