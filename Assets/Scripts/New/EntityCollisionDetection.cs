using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject wallHitParticle;
    [SerializeField] private GameObject entityHitParticle;
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Entity _thisEntity;

    private TurnSwitcher _turnSwitcher;
    private Vector3 _lastVelocity;

    private void Start()
    {
        _turnSwitcher = FindObjectOfType<TurnSwitcher>();
    }

    private void Update()
    {
        _lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        GameObject obj = collision.gameObject;
        float magnitude = _lastVelocity.magnitude;

        if (rb.velocity.magnitude > 6.5f)
        {
            FindObjectOfType<CameraShake>().Shake(1, .1f);
        }

        if (obj.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_lastVelocity == Vector3.zero) return;

            Vector3 newDirection = Vector3.Reflect(_lastVelocity.normalized, contact.normal);
            rb.velocity = newDirection * magnitude;

            Instantiate(wallHitParticle, contact.point, Quaternion.identity);
        }

        else if (obj.TryGetComponent(out Entity entity))
        {
            if (_turnSwitcher.CurrentEntity.IsMine == _thisEntity.IsMine != entity.IsMine)
            {
                float damage = Mathf.RoundToInt(_thisEntity.GetComponent<Rigidbody>().velocity.magnitude);
                damage = Mathf.Clamp(damage, _thisEntity.MinDamage, _thisEntity.MaxDamage);
                entity.TakeDamage(damage);
            }

            Instantiate(entityHitParticle, contact.point, Quaternion.identity);
        }
    }
}
