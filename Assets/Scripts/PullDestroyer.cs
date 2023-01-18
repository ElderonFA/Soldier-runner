using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPulled pulledObject))
        {
            pulledObject.Despawn();
        }
    }
}
