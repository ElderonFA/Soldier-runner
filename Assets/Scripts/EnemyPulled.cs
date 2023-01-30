using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MoveObject))]
public class EnemyPulled : MonoBehaviour, IPulled
{
    private void OnEnable()
    {
        PullController.respawnPulled?.Invoke(this);
    }
    
    public PulledType PulledType { get; } = PulledType.Enemy;

    public void Spawn(Vector3 position)
    {
        transform.position = position;
    }
    
    public void Despawn()
    {
        PullController.respawnPulled?.Invoke(this);
        var moveComponent = GetComponent<MoveObject>();
        moveComponent.RandomizeSpeed();
    }
}
