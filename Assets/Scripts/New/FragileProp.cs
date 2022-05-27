using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileProp : MonoBehaviour
{
    [SerializeField] private GameObject breakParticle;
    [SerializeField] private float health = 5;

    public float Health
    {
        get { return health; }
    }

    public void TakeDamage(float damage)
    {
        if (damage >= health)
        {
            Instantiate(breakParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else health -= damage;
    }
}
