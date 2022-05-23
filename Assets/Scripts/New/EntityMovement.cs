using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private float speedLimit = 5f;

    private Rigidbody _rb;
    private Vector3 _lastVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lastVelocity = _rb.velocity;
    }

    public void Move(float speed, Vector3 direction)
    {
        _rb.AddForce(direction * Mathf.Clamp(speed, 0, speedLimit) * 150);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_lastVelocity == Vector3.zero) return;

            ContactPoint contact = collision.GetContact(0);
            Vector3 newDirection = Vector3.Reflect(_lastVelocity.normalized, contact.normal);
            float magnitude = _rb.velocity.magnitude;
            _rb.velocity = newDirection * magnitude;
        }
    }
}