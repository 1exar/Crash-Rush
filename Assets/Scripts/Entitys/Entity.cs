using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IEffectsApplicator
{
    [SerializeField] private SpriteRenderer circleSprite;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private GameObject damageStar;
    [SerializeField] private Transform model;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private EntityMovement _movement;

    #region EffectCoroutines

    private Coroutine _fireProcces;

    #endregion
    
    private EntityContainer _container;

    private float _health;
    private float _minDamage;
    private float _maxDamage;
    private bool _isMine;

    private Vector3 _startScale;
    
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
        _rigidbody.mass = settings.weight;
        _rigidbody.drag = settings.drag;
        _movement.SpeedLimit = settings.maxSpeed;
    }
    
    private void Start()
    {
        _startScale = transform.localScale;
        
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
            Destroy(gameObject);
        }

        Transform star = Instantiate(damageStar, transform.position + Vector3.up * 2f, Quaternion.identity).transform;
        star.SetParent(transform);
        transform.DOShakeScale(.2f, .4f, 1);
        _health -= damage;
        healthSlider.value = _health;
        
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
        _fireProcces = StartCoroutine(FireProcces(dmg, duration));
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
