using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPulled : MonoBehaviour
{
    [SerializeField] 
    private PulledType pulledType;
    public PulledType PulledType => pulledType;
    
    public void Despawn()
    {
        PullController.respawnPulled?.Invoke(this);

        if (TryGetComponent(out MoveObject move)
        && pulledType == PulledType.Enemy)
        {
            move.RandomizeSpeed();
        }
    }
}

public enum PulledType
{
    Enemy,
    Tree,
}
