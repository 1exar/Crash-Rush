using UnityEngine;
using DG.Tweening;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private Transform _skinTransform;
    [SerializeField] private float _speedLimit = 5f;

    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    private Transform _thisObjectTransform;
    private Vector3 _canvasMainPos;

    private float _startSpeedLimit;
    
    private Vector3 _dir = new Vector3();
    
    public float SpeedLimit
    {
        get { return _speedLimit; }
        set
        {
            if (value > 0)
            {
                _speedLimit = value;
            }
            else
            {
                _speedLimit = 1;
            }
        }
    }

    public float StartSpeedLimit
    {
        private set {}
        get { return _startSpeedLimit; }
    }
    
    private void Start()
    {
        _startSpeedLimit = _speedLimit;
        _canvasMainPos = _canvasTransform.localPosition;
        _thisObjectTransform = transform;
    }
    
    private void Update()
    {
        _canvasTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        _canvasTransform.position = _thisObjectTransform.position + _canvasMainPos;

        if (_rb.velocity.magnitude < 1f)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            _skinTransform.DORotate(_skinTransform.rotation.eulerAngles + _dir, 1, RotateMode.WorldAxisAdd);
        }
    }

    public void Move(float speed, Vector3 direction)
    {
        _rb.AddForce(direction * Mathf.Clamp(speed, 0, _speedLimit) * 100);
    }
}