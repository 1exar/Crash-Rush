using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float health = 10;
    [SerializeField] private float damage = 5;
    [SerializeField] private bool isMine = false;

    private PathGenerator _pathGenerator = null;
    private EntityMovement _movement = null;
    private EntityContainer _container = null;

    public SpriteRenderer _circleSprite;
    
    public EntityMovement Movement
    {
        get { return _movement; }
    }

    public float Damage
    {
        get { return damage; }
    }
    public bool IsMine
    {
        get { return isMine; }
    }

    private void Start()
    {
        healthSlider.maxValue = health;

        healthSlider.value = health;
        
        _pathGenerator = FindObjectOfType<PathGenerator>();
        _movement = GetComponent<EntityMovement>();
        _container = FindObjectOfType<EntityContainer>();
    }

    public void TakeDamage(float damage)
    {
        if (damage >= health)
        {
            if (isMine) _container.PlayerEntities.Remove(this);
            else _container.EnemyEntities.Remove(this);
            Destroy(gameObject);
        }
        health -= damage;
        healthSlider.value = health;
    }

    public void PreviewDamage(float damage)
    {
        float mainValue = healthSlider.value;
        healthSlider.value = health - damage;
        healthSlider.value = mainValue;
    }
}
