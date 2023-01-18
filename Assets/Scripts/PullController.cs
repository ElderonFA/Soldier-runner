using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PullController : MonoBehaviour
{
    //public Action<Transform> respawnPulled;
    public static Action<IPulled> respawnPulled;
    void Start()
    {
        StartSpawnPulledObject();
        respawnPulled += RespawnOnePulled;
    }

    private void StartSpawnPulledObject()
    {
        var pulledObject = FindObjectsOfType<IPulled>();

        foreach (var pulled in pulledObject)
        {
            switch (pulled.PulledType)
            {
                case PulledType.Enemy:
                    transform.position = GeneratePositionWithType(PulledType.Enemy);
            
                    if (TryGetComponent(out MoveObject move))
                    {
                        move.RandomizeSpeed();
                    }
                    break;
                
                case PulledType.Tree:
                    transform.position = GeneratePositionWithType(PulledType.Tree);
                    break;
            }
        }
    }

    private void RespawnOnePulled(IPulled pulled)
    {
        pulled.transform.position = GeneratePositionWithType(pulled.PulledType);
    }

    private Vector3 GeneratePositionWithType(PulledType pulledType)
    {
        return pulledType switch
        {
            PulledType.Enemy => new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(20f, 40f)),
            PulledType.Tree => new Vector3(Random.Range(0, 2) == 0 ? 9f : -9f, 0f, Random.Range(20f, 60f)),
        };
    }
}