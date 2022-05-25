using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private float speedLimit = 5f;

    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    private Transform _thisObjectTransform;
    private Vector3 _canvasMainPos;

    private void Start()
    {
        _canvasMainPos = canvasTransform.localPosition;
        _thisObjectTransform = transform;
    }

    private void Update()
    {
        canvasTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        canvasTransform.position = _thisObjectTransform.position + _canvasMainPos;

        if (_rb.velocity.magnitude < 1f)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    public void Move(float speed, Vector3 direction)
    {
        _rb.AddForce(direction * Mathf.Clamp(speed, 0, speedLimit) * 100);
    }
}