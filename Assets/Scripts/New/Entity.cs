using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float health = 10;
    [SerializeField] private float damage = 5;
    [SerializeField] private bool isMine = false;

    private PathGenerator _pathGenerator = null;
    private EntityMovement _movement = null;

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
        _pathGenerator = FindObjectOfType<PathGenerator>();
        _movement = GetComponent<EntityMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (damage >= health) Destroy(gameObject);
        health -= damage;
    }
}
