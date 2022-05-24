using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private bool isMine;

    private EntityMovement_noRB _movement;
    private EntityContainer _container;

    public SpriteRenderer _circleSprite;
    
    public EntityMovement_noRB Movement
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
        _movement = GetComponent<EntityMovement_noRB>();
        _container = FindObjectOfType<EntityContainer>();

        healthSlider.maxValue = healthSlider.value = health;
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
}
