using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Entity : MonoBehaviour, IEffectsApplicator
{
    [SerializeField] private SpriteRenderer circleSprite;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private GameObject damageStar;
    [SerializeField] private Transform model;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private EntityMovement _movement;
    [SerializeField] private EntityCanvasController _canvasController;
    [SerializeField] private EntityType _currentType;
    
    #region EffectCoroutines

    private Coroutine _fireProcces;
    private Coroutine _freezeProcces;

    #endregion
    
    private EntityContainer _container;

   [SerializeField] private float _health;
   [SerializeField] private float _minDamage;
   [SerializeField] private float _maxDamage;
   [SerializeField] private bool _isMine;
   [SerializeField] private int _maxHealth;

    private Vector3 _startScale;


    public EntityType CurrentType
    {
        private set{}
        get { return _currentType; }
    }
    
    public Transform Model
    {
        get { return model; }
    }

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
        transform.Rotate(Vector3.up * 180);
        _isMine = isMine;
        
        _minDamage = settings.minDamage;
        _maxDamage = settings.maxDamage;
        _health = settings.health;
        _maxHealth = settings.health;
        _rigidbody.mass = settings.weight;
        _rigidbody.drag = settings.drag;
        _movement.SpeedLimit = settings.maxSpeed;
        if (isMine == false)
            _movement.SpeedLimit = 1;
    }
    
    private void Start()
    {
        _startScale = transform.localScale;
        
        _container = FindObjectOfType<EntityContainer>();

        _canvasController.SetHealthMax(_health);
    }

    public void TakeDamage(float damage)
    {
        _canvasController.ShowDamage(damage);
        
        if (damage >= _health)
        {
            _container.RemoveEntityFromList(this,_isMine);
            Instantiate(deathParticle, transform.position + Vector3.up, Quaternion.identity);
            Destroy(gameObject);
        }

        Transform star = Instantiate(damageStar, transform.position + Vector3.up * 2f, Quaternion.identity).transform;
        star.SetParent(transform);
        transform.DOShakeScale(.2f, .4f, 1);
        _health -= damage;
        _canvasController.SetHealth(_health);
        
        Invoke(nameof(ResetScaleToStart), .2f);
    }

    public virtual void AttackEntity(Entity _enemy, float dmg)
    {
        _enemy.TakeDamage(dmg);
    }
    
    private void ResetScaleToStart()
    {
        transform.localScale = _startScale;
    }

    public virtual void ApplyFireEffect(int dmg, int duration)
    {
        if(_fireProcces != null)
            StopCoroutine(_fireProcces);
        _fireProcces = StartCoroutine(FireProcces(dmg, duration));
    }

    public virtual void ApplyFrezzeEffect(int duration, float strength)
    {
        _freezeProcces = StartCoroutine(FreezeProcces(duration, strength));
    }

    public virtual void ApplyHealEffect(int hp)
    {
        _health += hp;
        if (_health > _maxHealth)
            _health = _maxHealth;
        _canvasController.SetHealth(_health);
    }

    private IEnumerator FreezeProcces(int duration,float strength)
    {
        _movement.SpeedLimit -= _movement.SpeedLimit / 100 * strength;
        yield return new WaitForSeconds(duration);
        _movement.SpeedLimit = _movement.StartSpeedLimit;
    }
    
    private IEnumerator FireProcces(int dmg, int duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            TakeDamage(dmg);
        }
    }
}
