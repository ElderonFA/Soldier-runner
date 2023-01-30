using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePulled : MonoBehaviour, IPulled
{
    private void OnEnable()
    {
        PullController.respawnPulled?.Invoke(this);
    }

    public PulledType PulledType { get; } = PulledType.Tree;

    public void Spawn(Vector3 position)
    {
        transform.position = position;
    }
    
    public void Despawn()
    {
        PullController.respawnPulled?.Invoke(this);
    }
}
