using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private SpriteRenderer circleSprite;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private EntityMovement _movement;
    
    private EntityContainer _container;

    private float _health;
    private float _minDamage;
    private float _maxDamage;
    private bool _isMine;

    public float MinDamage
    {
        get { return _minDamage; }
    }

    public float MaxDamage
    {
        get { return _maxDamage; }
    }

    public bool IsMine
    {
        get { return _isMine; }
    }

    public SpriteRenderer CircleSprite
    {
        get { return circleSprite; }
    }

    public void Init(EntitySettings settings, bool isMine)
    {
        _isMine = isMine;
        
        _minDamage = settings.minDamage;
        _maxDamage = settings.maxDamage;
        _health = settings.health;
        _rigidbody.mass = settings.weight;
        _rigidbody.drag = settings.drag;
        _movement.SpeedLimit = settings.maxSpeed;
    }
    
    private void Start()
    {
        _container = FindObjectOfType<EntityContainer>();

        healthSlider.maxValue = _health;
        healthSlider.value = _health;
    }

    public void TakeDamage(float damage)
    {
        if (damage >= _health)
        {
            if (_isMine) _container.PlayerEntities.Remove(this);
            else _container.EnemyEntities.Remove(this);
            Instantiate(deathParticle, transform.position + Vector3.up, Quaternion.identity);
            _audio.Play();
            Destroy(gameObject);
        }

        _health -= damage;
        healthSlider.value = _health;
    }
}
