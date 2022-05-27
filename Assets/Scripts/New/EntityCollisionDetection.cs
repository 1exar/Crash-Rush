using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionDetection : MonoBehaviour
{
    [SerializeField] private GameObject wallHitParticle;
    [SerializeField] private GameObject entityHitParticle;
    [SerializeField] private AudioSource hitAudioSource;
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

        hitAudioSource.Stop();
        hitAudioSource.Play();

        if (rb.velocity.magnitude > 6.5f)
        {
            FindObjectOfType<CameraShake>().Shake(1, .1f);
        }

        float damage = Mathf.RoundToInt(_thisEntity.GetComponent<Rigidbody>().velocity.magnitude);
        damage = Mathf.Clamp(damage, _thisEntity.MinDamage, _thisEntity.MaxDamage);

        if (obj.layer == LayerMask.NameToLayer("Wall"))
        {
            if (obj.TryGetComponent(out FragileProp prop))
            {
                prop.TakeDamage(damage);
                if (prop.Health <= damage)
                {
                    rb.velocity = _lastVelocity / 1.5f;
                    return;
                }
            }
            if (_lastVelocity == Vector3.zero) return;

            Vector3 newDirection = Vector3.Reflect(_lastVelocity.normalized, contact.normal);
            rb.velocity = newDirection * magnitude;

            Instantiate(wallHitParticle, contact.point, Quaternion.identity);
        }

        else if (obj.TryGetComponent(out Entity entity))
        {
            if (_turnSwitcher.CurrentEntity.IsMine != entity.IsMine)
            {
                entity.TakeDamage(damage);
            }

            Instantiate(entityHitParticle, contact.point, Quaternion.identity);
        }
    }
}
