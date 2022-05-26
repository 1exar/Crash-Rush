using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private float _speedLimit = 5f;

    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    private Transform _thisObjectTransform;
    private Vector3 _canvasMainPos;

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
    
    private void Start()
    {
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
    }

    public void Move(float speed, Vector3 direction)
    {
        _rb.AddForce(direction * Mathf.Clamp(speed, 0, _speedLimit) * 100);
    }
}