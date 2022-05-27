using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMover : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    private void Update()
    {
        if (_rigidbody.velocity.magnitude < 4f)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
