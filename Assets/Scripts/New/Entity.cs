using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float health;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private bool isMine;
    [SerializeField] private SpriteRenderer circleSprite;

    private EntityContainer _container;

    public float MinDamage
    {
        get { return minDamage; }
    }

    public float MaxDamage
    {
        get { return maxDamage; }
    }

    public bool IsMine
    {
        get { return isMine; }
    }

    public SpriteRenderer CircleSprite
    {
        get { return circleSprite; }
    }

    private void Start()
    {
        _container = FindObjectOfType<EntityContainer>();

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void TakeDamage(float damage)
    {
        if (damage >= health)
        {
            if (isMine) _container.PlayerEntities.Remove(this);
            else _container.EnemyEntities.Remove(this);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        health -= damage;
        healthSlider.value = health;
    }
}
