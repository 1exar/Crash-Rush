using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiBlast : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private Transform[] spawnPoints;

    public void Blast()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(particle, point.position, Quaternion.identity);
        }
    }
}
