using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private GameObject wallHitParticle;
    [SerializeField] private GameObject entityHitParticle;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private float speedLimit = 5f;

    private TurnSwitcher _turnSwitcher;
    private Entity _thisEntity;
    private Rigidbody _rb;
    private Vector3 _lastVelocity;

    private void Awake()
    {
        _thisEntity = GetComponent<Entity>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    private void Update()
    {
        canvasTransform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        _lastVelocity = _rb.velocity;

        if (_rb.velocity.magnitude < 1f)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    public void Move(float speed, Vector3 direction)
    {
        _rb.AddForce(direction * Mathf.Clamp(speed, 0, speedLimit) * 200);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        GameObject obj = collision.gameObject;
        float magnitude = _lastVelocity.magnitude;

        if (obj.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_lastVelocity == Vector3.zero) return;

            Vector3 newDirection = Vector3.Reflect(_lastVelocity.normalized, contact.normal);
            _rb.velocity = newDirection * magnitude;

            Instantiate(wallHitParticle, contact.point, Quaternion.identity);
        }

        else if (obj.TryGetComponent(out Entity entity))
        {
            if (_turnSwitcher.CurrentEntity.IsMine == _thisEntity.IsMine != entity.IsMine)
            {
                entity.TakeDamage(_thisEntity.Damage);
            }

            Instantiate(entityHitParticle, contact.point, Quaternion.identity);
        }

        FindObjectOfType<CameraShake>().Shake(magnitude / 10, .1f);
    }
}